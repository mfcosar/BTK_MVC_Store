using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class CategoryController: Controller
    {
        private readonly IServiceManager _manager;
        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var categories   = _manager.CategoryService.GetAllCategories(false);
            ViewBag.Products = _manager.ProductService.GetAllProducts(false);

            return View(categories);            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([FromForm] Category category)
        {
            if (ModelState.IsValid) { 
                _manager.CategoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
