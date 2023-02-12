using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eMovies.Data.Base
{
	public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
	{
		private readonly AppDbContext _Context;

		public EntityBaseRepository(AppDbContext context) 
		{
			_Context = context;
		}	

		public async Task AddAsync(T entity)
		{
			await _Context.Set<T>().AddAsync(entity);
			await _Context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
			EntityEntry entityEntry = _Context.Entry<T>(entity);
			entityEntry.State = EntityState.Deleted;
			await _Context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync() => await _Context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id) => await _Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
		
		public async Task UpdateAsync(int id, T entity)
		{
			EntityEntry entityEntry = _Context.Entry<T>(entity);
			entityEntry.State = EntityState.Modified;
			await _Context.SaveChangesAsync();
		}
	}
}
