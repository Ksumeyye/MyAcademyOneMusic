using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminAboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly UserManager<AppUser> _userManager;

        public AdminAboutController(IAboutService aboutService, UserManager<AppUser> userManager)
        {
            _aboutService = aboutService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["username"] = user.Name + " " + user.Surname; //Tempdata geçici süre hafızaya almaya yarar.
            var values = _aboutService.TGetList();
            return View(values);
        }
        public IActionResult DeleteAbout(int id)
        {
            _aboutService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            _aboutService.TCreate(about);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var values=_aboutService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About about) 
        {
        _aboutService.TUpdate(about);
            return RedirectToAction("Index");
        }
    }
}
