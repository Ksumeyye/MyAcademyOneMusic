using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")] // Sadece rolü artist olan kişiler bu controller'a erişebilir. 
    [Route("[area]/[controller]/[action]/{id?}")] //burası bir area içerisinde olduğu için area içerisindeki controllerlara bu route 'ı belirtiriz
    public class MySongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
