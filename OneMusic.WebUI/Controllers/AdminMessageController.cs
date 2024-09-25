using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Controllers
{
    public class AdminMessageController : Controller
    {
        private readonly IMessageService _messageService;
        public AdminMessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public IActionResult Index()
        {
            var values=_messageService.TGetList();
            return View(values);
        }
    }
}
