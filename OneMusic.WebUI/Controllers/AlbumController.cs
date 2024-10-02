using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class AlbumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}