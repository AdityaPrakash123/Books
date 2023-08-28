using Books.DataAccess.Data;
using Books.DataAccess.Repository;
using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _categoryRepository.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Category category = _categoryRepository.Get(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        /*public IActionResult Delete(int id)
        {
            // Get the category to delete
            Category category = _db.Categories.SingleOrDefault(c => c.Id ==id);
            return View(category);
        }*/

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = _categoryRepository.Get(id);
            return RedirectToAction("Index");
        }
    }
}