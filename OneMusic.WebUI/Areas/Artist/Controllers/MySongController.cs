using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")] // Sadece rolü artist olan kişiler bu controller'a erişebilir. 
    [Route("[area]/[controller]/[action]/{id?}")] //burası bir area içerisinde olduğu için area içerisindeki controllerlara bu route 'ı belirtiriz
    public class MySongController : Controller
    {
        private readonly ISongService _songService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;
        private readonly OneMusicContext _oneMusicContext;
        public MySongController(ISongService songService, UserManager<AppUser> userManager, IAlbumService albumService, OneMusicContext oneMusicContext)
        {
            _songService = songService;
            _userManager = userManager;
            _albumService = albumService;
            _oneMusicContext = oneMusicContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userid = user.Id;
            var values = _songService.TGetSongswithAlbumByUserId(userid).ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateSong()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var albumList = _albumService.TGetAlbumsByArtist(user.Id);
            List<SelectListItem> albums = (from x in albumList
                                           select new SelectListItem
                                           {
                                               Text = x.AlbumName,
                                               Value = x.AlbumId.ToString()
                                           }).ToList();
            ViewBag.albums = albums;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(SongViewModel s)
        {
            if (s.SongImageUrl != null && s.SongFile != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(s.SongImageUrl.FileName);
                var imagename = ($"{Guid.NewGuid()}{extension}");
                var savelocation = ($"{resource}/wwwroot/images/{imagename}");
                var stream = new FileStream(savelocation, FileMode.Create);
                await s.SongImageUrl.CopyToAsync(stream);

                var resource1 = Directory.GetCurrentDirectory();
                var extension1 = Path.GetExtension(s.SongFile.FileName);
                var songname = ($"{Guid.NewGuid()}{extension1}");
                var savelocation1 = ($"{resource1}/wwwroot/songs/{songname}");
                var stream1 = new FileStream(savelocation1, FileMode.Create);
                await s.SongFile.CopyToAsync(stream1);

                Song son = new Song()
                {
                    SongName = s.SongName,
                    SongImageUrl = imagename,
                    SongUrl = songname,
                    AlbumId = s.AlbumId

                };

                _songService.TCreate(son);
                return RedirectToAction("Index");

            }

            return View();
        }

        public IActionResult DeleteSong(int id)
        {
            _songService.TDelete(id);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult UpdateSong(int id)
        {
            var values = _songService.TGetById(id);

            List<SelectListItem> list = (from x in _oneMusicContext.Albums.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AlbumName,
                                             Value = x.AlbumId.ToString()
                                         }).ToList();

            ViewBag.Albums = list;

            var model = new SongUpdateViewModel()
            {
                Id = values.SongId,
                ImageUrl = values.SongImageUrl,
                SongFileUrl = values.SongUrl,
                SongName = values.SongName,
                AlbumId = values.AlbumId

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSong(SongUpdateViewModel s)
        {
            if (s.SongFileUrl != null || s.ImageUrl != null)
            {
                // Mevcut kaydı bul
                var existingSong = _songService.TGetById(s.Id);

                // Eğer mevcut kayıt null değilse, güncelleme işlemini gerçekleştir
                if (existingSong != null)
                {
                    if (s.SongImageUrl != null)
                    {
                        var resource1 = Directory.GetCurrentDirectory();
                        var extension1 = Path.GetExtension(s.SongImageUrl.FileName);
                        var imagename1 = ($"{Guid.NewGuid()}{extension1}");
                        var savelocation1 = ($"{resource1}/wwwroot/images/{imagename1}");
                        var stream1 = new FileStream(savelocation1, FileMode.Create);
                        await s.SongImageUrl.CopyToAsync(stream1);
                        // Mevcut kaydı güncelle
                        existingSong.SongName = s.SongName;
                        existingSong.SongImageUrl = imagename1;
                        existingSong.AlbumId = s.AlbumId;
                    }
                    else if (s.SongFile != null)
                    {
                        var resource2 = Directory.GetCurrentDirectory();
                        var extension2 = Path.GetExtension(s.SongFile.FileName);
                        var songname2 = ($"{Guid.NewGuid()}{extension2}");
                        var savelocation2 = ($"{resource2}/wwwroot/audio/{songname2}");
                        var stream2 = new FileStream(savelocation2, FileMode.Create);
                        await s.SongFile.CopyToAsync(stream2);
                        // Mevcut kaydı güncelle
                        existingSong.SongName = s.SongName;
                        existingSong.SongUrl = songname2;
                        existingSong.AlbumId = s.AlbumId;

                    }
                    else if (s.SongImageUrl == null && s.SongImageUrl == null)
                    {
                        // Mevcut kaydı güncelle
                        existingSong.SongName = s.SongName;
                        existingSong.SongImageUrl = s.ImageUrl;
                        existingSong.SongUrl = s.SongFileUrl;
                        existingSong.AlbumId = s.AlbumId;

                    }
                    _songService.TUpdate(existingSong);

                }

                return RedirectToAction("Index");
            }

            return View();

        }
    }
}
