using eMovies.Data.Base;
using eMovies.Models;

namespace eMovies.Data.Services
{
	public class ActorsService : EntityBaseRepository<Actor>, IActorsService
	{
		public ActorsService(AppDbContext context) : base(context)
		{
		}
	}
}
