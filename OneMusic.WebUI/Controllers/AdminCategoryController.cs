using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var values=_categoryService.TGetList();
            return View(values);
        }
        public IActionResult DeleteCategory(int id)
        {
            _categoryService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _categoryService.TCreate(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var values= _categoryService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category) 
        {
        _categoryService.TUpdate(category);
            return RedirectToAction("Index");
        }
    }
}
