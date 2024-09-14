using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Areas.Artist.Controllers
    
{
    [Area("Artist")] // Area içerisindeki artist controller
    [Authorize(Roles ="Artist")] // Sadece rolü artist olan kişiler bu controller'a erişebilir. 
    [Route("[area]/[controller]/[action]/{id?}")] //burası bir area içerisinde olduğu için area içerisindeki controllerlara bu route 'ı belirtiriz
    public class MyAlbumController(IAlbumService _albumService,UserManager<AppUser> _userManager): Controller  //.Net8 ile gelen primary constructor
	{
        public async Task <IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userid = user.Id;
            var values = _albumService.TGetAlbumsByArtist(userid);
            return View(values);
        }
    }
}
