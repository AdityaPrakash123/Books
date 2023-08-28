using Books.DataAccess.Data;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories.ToList();
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
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(/*category*/);
        }

        public IActionResult Edit(int id)
        {
            Category category = _db.Categories.SingleOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
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
            _db.Categories.Remove(_db.Categories.SingleOrDefault(c => c.Id == id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}