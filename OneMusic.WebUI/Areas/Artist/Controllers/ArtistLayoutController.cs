using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    public class ArtistLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
