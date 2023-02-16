using eMovies.Data.Base;
using eMovies.Data.ViewModels;
using eMovies.Models;

namespace eMovies.Data.Services
{
	public interface IMoviesService : IEntityBaseRepository<Movie>
	{
		Task<Movie> GetMovieByIdAsync(int id);
		Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
		Task AddNewMovieAsync(NewMovieVM movie);
		Task UpdateMovieAsync(NewMovieVM movie);
	}
}
