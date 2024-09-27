using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISongService _songService;
        private readonly ICategoryService _categoryService;
        public AdminAlbumController(IAlbumService albumService, UserManager<AppUser> userManager, ISongService songService, ICategoryService categoryService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _songService = songService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values = _albumService.TGetAlbumswithArtist();
            return View(values);
        }
        public IActionResult DeleteAlbum(int id)
        {
            _albumService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateAlbum()
        {
            var categories = _categoryService.TGetList();
            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();
            ViewBag.CategoryList = categoryList;

            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            ViewBag.Singers = artists.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = $"{b.Name} {b.Surname}"
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateAlbum(Album album)
        {
            _albumService.TCreate(album);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAlbum(int id)
        {
            var categories = _categoryService.TGetList();
            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();
            ViewBag.CategoryList = categoryList;
            var values = _albumService.TGetById(id);
            //Sanatçıları viewbagin içine koydum
            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            ViewBag.SingerId = artists.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = $"{b.Name} {b.Surname}"
            }).ToList();
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateAlbum(Album album)
        {
            _albumService.TUpdate(album);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AlbumByArtist(int artistId)
        {
            var artist = await _userManager.FindByIdAsync(artistId.ToString());
            var albums = _albumService.TGetAlbumsByArtist(artistId);
            var model = new AlbumsByArtistViewModel
            {
                Artist = artist,
                Albums = albums,
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetSongsByAlbumId(int albumId)
        { //Album ıd'e göre şarkı getirdim
            var songs = _songService.TGetSongsByAlbumId(albumId);
            var model = new AlbumsByArtistViewModel
            {
                Songs = songs
            };
            return View(model);
        }
    }
    public class AlbumsByArtistViewModel
    {
        public AppUser Artist { get; set; }
        public Album Album { get; set; }

        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
    }
}
