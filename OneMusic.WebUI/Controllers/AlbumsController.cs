using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class AlbumsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;
        private readonly OneMusicContext _oneMusicContext;

        public AlbumsController(ICategoryService categoryService, UserManager<AppUser> userManager, IAlbumService albumService, OneMusicContext oneMusicContext)
        {
            _categoryService = categoryService;
            _userManager = userManager;
            _albumService = albumService;
            _oneMusicContext = oneMusicContext;
        }

        public async Task<IActionResult> Index()
        {
            var categoryList = _categoryService.TGetList();
            var artistList = await _userManager.GetUsersInRoleAsync("Artist");

            List<SelectListItem> artists = (from x in artistList
                                            select new SelectListItem
                                            {
                                                Text = x.Name + " " + x.Surname,
                                                Value = x.Name + " " + x.Surname,
                                            }).ToList();

            ViewBag.artists = artists;

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryName,
                                               }).ToList();

            ViewBag.categories = categories;

            if (TempData["filterAlbums"] != null)
            {


                var filterList = TempData["filterAlbums"].ToString();

                var albums = JsonSerializer.Deserialize<List<Album>>(filterList, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                if (albums != null)
                {
                    return View(albums);
                }

            }

            var values = _albumService.TGetAlbumswithArtist();
            return View(values);
        }

        [HttpGet]
        public async Task<PartialViewResult> FilterAlbums()
        {
            var categoryList = _categoryService.TGetList();
            var artistList = await _userManager.GetUsersInRoleAsync("Artist");

            List<SelectListItem> artists = (from x in artistList
                                            select new SelectListItem
                                            {
                                                Text = x.Name + " " + x.Surname,
                                                Value = x.Name + " " + x.Surname,
                                            }).ToList();

            ViewBag.artists = artists;

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryName,
                                               }).ToList();

            ViewBag.categories = categories;

            return PartialView();
        }

        [HttpPost]
        public IActionResult FilterAlbums(string category, string artist)
        {
            if (!string.IsNullOrEmpty(category) || !string.IsNullOrEmpty(artist))
            {
                var values = _oneMusicContext.Albums.Include(x => x.AppUser).Include(x => x.Category).Where(x => x.Category.CategoryName == category && x.AppUser.Name + " " + x.AppUser.Surname == artist).ToList();

                TempData["filterAlbums"] = JsonSerializer.Serialize(values, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

            }



            return RedirectToAction("Index");
        }

    }
}