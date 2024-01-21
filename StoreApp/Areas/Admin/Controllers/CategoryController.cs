using Entities.Models;
using Entities.Dtos;
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
            var model = _manager.CategoryService.GetOneCategoryForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update( [FromForm] CategoryDtoForUpdate categoryDto)
        {
            if (ModelState.IsValid) { 
                _manager.CategoryService.UpdateOneCategory(categoryDto);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([FromForm] CategoryDtoForInsertion categoryDto)
        {
            if (ModelState.IsValid) { 
                _manager.CategoryService.CreateCategory(categoryDto);
                return RedirectToAction("Index");
            }

            return View();
        }
        // featctegory branch is merged with master, categorucontroller is working without problem
    }
}
