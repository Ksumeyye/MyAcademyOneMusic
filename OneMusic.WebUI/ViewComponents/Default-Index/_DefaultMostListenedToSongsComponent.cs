using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultMostListenedToSongsComponent : ViewComponent
    {
        private readonly ISongService _songService;
        public _DefaultMostListenedToSongsComponent(ISongService songService)
        {
            _songService = songService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _songService.TGetSongWithAlbum().OrderByDescending(x => x.SongValue).Take(6).ToList();
            return View(values);
        }
    }
}
