using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

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

        [HttpGet]
        public IActionResult Delete([FromRoute(Name ="id")]int id)
        {
            _manager.CategoryService.DeleteOneCategory(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update([FromRoute(Name ="id")]int id)
        {
            var model = _manager.CategoryService.GetOneCategory(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update( Category category)
        {
            if (ModelState.IsValid) { 
                _manager.CategoryService.UpdateOneCategory(category);
                return RedirectToAction("Index");
            }
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
