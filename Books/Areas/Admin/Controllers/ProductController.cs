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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
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

            ProductVM productVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM, IFormFile? file)  // The file uploaded is received here and it must be uploaded in the images/product folder
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; // Path of the wwwrootfolder
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Random image name
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); // This is the path of the product folder where the file should be uploaded

                    using (FileStream fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream); // Copies the file to the location
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                
                _productRepository.Add(productVM.Product);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                productVM.CategoryList = CategoryList;
                return View(productVM);
            }
        }

        public IActionResult Details(int id)
        {
            Product product = _productRepository.Get(x => x.Id == id);
            return View(product);
        }


        public IActionResult Edit(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _productRepository.Get(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text= x.Name,
                Value = x.Id.ToString()
            });


            ProductVM productVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = product

            };
                                    
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (FileStream fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                                                
                _productRepository.Update(productVM.Product);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                productVM.CategoryList = CategoryList;
                return View(CategoryList);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            Product product = _productRepository.Get(u => u.Id == id);

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if(!String.IsNullOrEmpty(product.ImageUrl))
            {
                var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _productRepository.Remove(product);
            _productRepository.Save();
            return RedirectToAction("Index");
        }
    }
}