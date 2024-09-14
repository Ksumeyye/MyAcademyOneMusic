using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
		public IActionResult Error404()
		{
			return View();
		}
	}
}
