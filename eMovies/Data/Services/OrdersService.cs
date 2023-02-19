using eMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace eMovies.Data.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly AppDbContext _context;

		public OrdersService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
		{
			var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(m => m.Movie).Where(n => n.UserId == userId).ToListAsync();
			return orders;
		}

		public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
		{
			var order = new Order()
			{
				UserId = userId,
				Email = userEmailAddress
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			foreach (var item in items) 
			{
				var orderItem = new OrderItem() 
				{
					Amount = item.Amount,
					Price = item.Movie.Price,
					MovieId = item.Movie.Id,
					OrderId = order.Id					
				};
				await _context.OrderItems.AddAsync(orderItem);
			}
			await _context.SaveChangesAsync();
		}
	}
}
