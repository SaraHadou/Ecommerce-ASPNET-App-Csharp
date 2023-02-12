using eMovies.Data.Base;
using eMovies.Models;

namespace eMovies.Data.Services
{
	public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
	{
		public CinemasService(AppDbContext context) : base(context)
		{
		}
	}
}
