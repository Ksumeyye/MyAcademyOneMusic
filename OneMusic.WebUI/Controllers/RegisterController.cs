using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]  //aouthorize'dan muaf

    public class RegisterController : Controller
    {
        public readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterViewModel model)
        {
            AppUser user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname
            };
            if(model.Password==model.ConfirnPassword) 
            {
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Login");
				}

				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
            return View();
        }
    }
}
