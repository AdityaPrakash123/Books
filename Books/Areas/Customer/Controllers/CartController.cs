using Books.DataAccess.Repository;
using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Books.Areas.Customer.Controllers
{
	[Area("customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IShoppingCartRepository _shoppingCartRepository;

        public CartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


			ShoppingCartVM shoppingCart = new()
			{
				ShoppingCartList = _shoppingCartRepository.GetAll(u => u.ApplicationUserId == userId)
			};

			double total = 0;
			foreach(var cart in shoppingCart.ShoppingCartList)
			{
				total += cart.Product.Price;
			}

			shoppingCart.OrderTotal = total;

			return View(shoppingCart);

        }

		public IActionResult Add(int cartId)
		{

			var cartFromDb = _shoppingCartRepository.Get(u => u.Id == cartId);
			cartFromDb.Count++;
			_shoppingCartRepository.Update(cartFromDb);
			_shoppingCartRepository.Save();
			return RedirectToAction("Index");
		}

        public IActionResult Minus(int cartId)
        {

            var cartFromDb = _shoppingCartRepository.Get(u => u.Id == cartId);
			if(cartFromDb.Count <= 1)
			{
                _shoppingCartRepository.Remove(cartFromDb);
			}
			else
			{
                cartFromDb.Count--;
                _shoppingCartRepository.Update(cartFromDb);
            }
            _shoppingCartRepository.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _shoppingCartRepository.Get(u => u.Id == cartId);
            _shoppingCartRepository.Remove(cartFromDb);
            _shoppingCartRepository.Save();
            return RedirectToAction("Index");
        }
    }
}