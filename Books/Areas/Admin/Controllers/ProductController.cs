using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            // Also need to pass list of Categories
            return View(products);
        }

        public IActionResult Create()
        {

            IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                _productRepository.Add(productVM.Product);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            return View(productVM);
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