using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultPopularArtistComponent: ViewComponent
    {
        private readonly ISongService _songService;
        public _DefaultPopularArtistComponent(ISongService songService)
        {
            _songService = songService;
        }
        public IViewComponentResult Invoke()
        {
            var values=_songService.TGetList().OrderBy(x=>x.SongId).Take(1).ToList();
            return View(values);
        }
    }
}
