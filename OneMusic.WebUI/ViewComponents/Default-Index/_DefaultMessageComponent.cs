using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultMessageComponent : ViewComponent
    {
        private readonly IMessageService _messageService;

        public _DefaultMessageComponent(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}