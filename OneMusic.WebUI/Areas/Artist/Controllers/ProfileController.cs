using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")] // Sadece rolü artist olan kişiler bu controller'a erişebilir. 
    [Route("[area]/[controller]/[action]/{id?}")] //burası bir area içerisinde olduğu için area içerisindeki controllerlara bu route 'ı belirtiriz
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new ArtistEditViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ArtistEditViewModel model)
        {
            ModelState.Clear();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(model.ImageFile != null) 
            {
                var resource=Directory.GetCurrentDirectory();
                var extension=Path.GetExtension(model.ImageFile.FileName).ToLower();
                if(extension !=".jpg" && extension !=".jpeg" && extension !=".png")
                {
                    //Desteklenmeyen dosya uzantısı hatası
                    ModelState.AddModelError("ImageFile", "Sadece resim dosyaları kabul edilir.");
                    //Gerekirse, işlemi sonlandırabilirsiniz.
                    return View(model);
                }
                var imageName=Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/images/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);
                user.ImageUrl = "/images/" + imageName;
            }
            
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber=model.PhoneNumber;
            user.Email = model.Email;
            user.UserName=model.UserName;

            var result = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if(result == true) 
            {
                if (model.NewPassword != null && model.ConfirmPassword == model.NewPassword)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var item in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            return View();
                        }
                    }
                }
                var updateResult= await _userManager.UpdateAsync(user);
                if(updateResult.Succeeded) 
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            ModelState.AddModelError("", "Mevcut Şifreniz Hatalı");
            return View();
        }

    }
}
