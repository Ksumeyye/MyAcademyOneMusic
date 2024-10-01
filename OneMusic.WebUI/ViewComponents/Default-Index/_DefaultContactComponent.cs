using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultContactComponent: ViewComponent
    {
        private readonly IContactService _contactService;
        public _DefaultContactComponent(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IViewComponentResult Invoke()
        {
            var values=_contactService.TGetList();
            return View(values);
        }
    }
}
