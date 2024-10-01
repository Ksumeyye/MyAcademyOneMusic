using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultAlbumSongSaleComponent:ViewComponent
    {
        private readonly IAlbumService _albumService;

        public _DefaultAlbumSongSaleComponent(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        public IViewComponentResult Invoke()
        {
            var values=_albumService.TGetAlbumswithArtist();
            return View(values);
        }
    }
}
