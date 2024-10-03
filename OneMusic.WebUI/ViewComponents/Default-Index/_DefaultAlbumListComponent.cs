using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
	public class _DefaultAlbumListComponent: ViewComponent
	{
		private readonly ISongService __songService;
		public _DefaultAlbumListComponent(ISongService songService)
		{
			__songService = songService;
		}
		public IViewComponentResult Invoke()
		{
			var values = __songService.TGetSongsWithAlbumAndArtist();
			return View(values);
		}
	}
}
