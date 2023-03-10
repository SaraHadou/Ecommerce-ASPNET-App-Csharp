using eMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace eMovies.Data.Cart
{
	public class ShoppingCart
	{
		public AppDbContext _context { get; set; }
		public string ShoppingCartId { get; set; }
		public List<ShoppingCartItem> ShoppingCartItems { get; set; }
		public static ISession Session { get; set; }

		public ShoppingCart(AppDbContext context)
		{
			_context = context;
		}

		public static ShoppingCart GetShoppingCart(IServiceProvider services)
		{
			Session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<AppDbContext>();
			
			string cartId = Session.GetString("CartId") ?? Guid.NewGuid().ToString();
			Session.SetString("CartId", cartId);

			return new ShoppingCart(context) { ShoppingCartId = cartId };
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());	
		}

		public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();

		public void AddItemToCart(Movie movie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.ShoppingCartId == ShoppingCartId && n.Movie.Id == movie.Id);
			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem()
				{
					ShoppingCartId = ShoppingCartId,
					Movie = movie,
					Amount = 1
				};
				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}
			_context.SaveChanges();
		}

		public void RemoveItemFromCart(Movie movie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.ShoppingCartId == ShoppingCartId && n.Movie.Id == movie.Id);
			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}
			_context.SaveChanges();
		}

		public async Task ClearShoppingCartAsync()
		{
			var shoppingCartItems = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
			_context.ShoppingCartItems.UpdateRange(shoppingCartItems);

			Session.Remove("CartId");

			await _context.SaveChangesAsync();	
		}
	}
}
