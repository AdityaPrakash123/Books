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

		
	}
}
