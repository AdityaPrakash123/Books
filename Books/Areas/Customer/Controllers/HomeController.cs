using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Books.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _productRepository.Get(x => x.Id == id);
            ShoppingCart shoppingCart = new()
            {
                Product = product,
                Count = 1,
                ProductId = id
            };
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // When adding the shopping cart to the shopping cart table, the id of the user who is signed in is required
            /*var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;*/

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId; // Adding the user which added the product

            ShoppingCart cart = _shoppingCartRepository.Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

            if (cart != null)
            {
                // shopping cart already exists
                cart.Count += shoppingCart.Count;
                _shoppingCartRepository.Update(cart);
            }
            else
            {
                // add cart
                _shoppingCartRepository.Add(shoppingCart);

            }
            _shoppingCartRepository.Save();
            return RedirectToAction("Index");
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}