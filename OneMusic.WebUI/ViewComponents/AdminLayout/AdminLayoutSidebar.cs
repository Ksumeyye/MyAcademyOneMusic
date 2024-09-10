using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.ViewComponents.AdminLayout
{
    public class _AdminLayoutSidebar: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
