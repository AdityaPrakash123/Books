using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                _productRepository.Add(product);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _productRepository.Get(u => u.Id == id);
            
            if(product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.Get(u => u.Id == id);
            _productRepository.Remove(product);
            _productRepository.Save();
            return RedirectToAction("Index");
        }


    }
}
