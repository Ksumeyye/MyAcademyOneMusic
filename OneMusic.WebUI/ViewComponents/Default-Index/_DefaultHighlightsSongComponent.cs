using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultHighlightsSongComponent: ViewComponent
    {
        private readonly ISongService _songService;
        public _DefaultHighlightsSongComponent(ISongService songService)
        {
            _songService = songService;
        }
        public IViewComponentResult Invoke()
        {
            var values=_songService.TGetSongWithAlbum().ToList();
            var random= new Random();
            var randomValues=values.OrderBy(x=>random.Next()).Take(5).ToList();
            return View(randomValues);
        }
    }
}
