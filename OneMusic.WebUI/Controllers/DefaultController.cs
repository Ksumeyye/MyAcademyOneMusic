using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController(IMessageService _messageService,IAlbumService _albumService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            _messageService.TCreate(message);
            return NoContent();
        }
    }
}
