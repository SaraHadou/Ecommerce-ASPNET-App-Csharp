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
		private readonly IOrdersService _ordersService;

		public OrdersController(ShoppingCart shoppingCart, IMoviesService moviesService, IOrdersService ordersService)
		{
			_shoppingCart = shoppingCart;
			_moviesService = moviesService;
			_ordersService = ordersService;
		}

		public async Task<IActionResult> Index()
		{
			string userId = "";
			string userRole = "";

			var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
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

		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string userId = "";
			string userRole = "";

			await _ordersService.StoreOrderAsync(items, userId, userRole);
			await _shoppingCart.ClearShoppingCartAsync();

			return View("OrderCompleted");
		}
	}
}