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
        [HttpGet]
        public IActionResult CreateAlbum()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> CreateAlbum(Album album)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            album.AppUserId = user.Id;
            _albumService.TCreate(album);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAlbum(int id)
        {
            _albumService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAlbum(int id)
        {
            var values=_albumService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAlbum(Album album)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            album.AppUserId = user.Id;
            _albumService.TUpdate(album);
            return RedirectToAction("Index");
        }
    }
}
