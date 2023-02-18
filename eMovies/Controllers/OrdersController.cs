using eMovies.Data.Cart;
using eMovies.Data.Services;
using eMovies.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eMovies.Controllers
{
	public class OrdersController : Controller
	{
		private readonly ShoppingCart _shoppingCart;
		private readonly IMoviesService _moviesService;

		public OrdersController(ShoppingCart shoppingCart, IMoviesService moviesService)
		{
			_shoppingCart = shoppingCart;
			_moviesService = moviesService;
		}

		public IActionResult ShoppingCart()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;
			var response = new ShoppingCartVM() 
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};
			return View(response);
		}

		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);
			if (item != null)
			{
				_shoppingCart.AddItemToCart(item);
			}
			return RedirectToAction("ShoppingCart");
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);
			if (item != null)
			{
				_shoppingCart.RemoveItemFromCart(item);
			}
			return RedirectToAction("ShoppingCart");
		}
	}
}