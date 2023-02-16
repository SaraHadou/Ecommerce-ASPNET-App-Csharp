using eMovies.Data.Base;
using eMovies.Data.Enums;
using eMovies.Data.ViewModels;
using eMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace eMovies.Data.Services
{
	public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
	{
		private readonly AppDbContext _context;

		public MoviesService(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task AddNewMovieAsync(NewMovieVM movie)
		{
			var newMovie = new Movie()
			{
				Name = movie.Name,
				Description = movie.Description,
				Price = movie.Price,
				StartDate = movie.StartDate,
				EndDate = movie.EndDate,
				ImageURL = movie.ImageURL,
				MovieCategory = movie.MovieCategory,
				CinemaId = movie.CinemaId,	
				ProducerId = movie.ProducerId
			};
			await _context.Movies.AddAsync(newMovie);
			await _context.SaveChangesAsync();

			foreach (var actorId in movie.ActorIds)
			{
				var newActorMovie = new Actor_Movie()
				{
					MovieId = newMovie.Id,
					ActorId = actorId,
				};
				await _context.Actors_Movies.AddAsync(newActorMovie);
			}
			await _context.SaveChangesAsync();
		}

		public async Task<Movie> GetMovieByIdAsync(int id)
		{
			var movieDetails = await _context.Movies
				.Include(c => c.Cinema)
				.Include(p => p.Producer)
				.Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
				.FirstOrDefaultAsync(m => m.Id == id);
			return movieDetails;
		}

		public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
		{
			var response = new NewMovieDropdownsVM()
			{
				Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
				Producers = await _context.Producers.OrderBy(a => a.FullName).ToListAsync(),
				Cinemas = await _context.Cinemas.OrderBy(a => a.Name).ToListAsync(),

			};
			return response;
		}

		public async Task UpdateMovieAsync(NewMovieVM movie)
		{
			var selectedMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);
			if (selectedMovie != null) 
			{ 
				selectedMovie.Name = movie.Name;
				selectedMovie.Description = movie.Description;
				selectedMovie.Price = movie.Price;
				selectedMovie.StartDate = movie.StartDate;
				selectedMovie.EndDate = movie.EndDate;
				selectedMovie.ImageURL = movie.ImageURL;
				selectedMovie.MovieCategory = movie.MovieCategory;
				selectedMovie.CinemaId = movie.CinemaId;
				selectedMovie.ProducerId = movie.ProducerId;
				await _context.SaveChangesAsync();
			}

			var selectedMovieActors = await _context.Actors_Movies.Where(am => am.MovieId == movie.Id).ToListAsync();
			_context.Actors_Movies.RemoveRange(selectedMovieActors);
			await _context.SaveChangesAsync();

			foreach(var actorId in movie.ActorIds)
			{
				var newActorMovie = new Actor_Movie()
				{
					MovieId = movie.Id,
					ActorId = actorId,
				};
				await _context.Actors_Movies.AddAsync(newActorMovie);
			}
			await _context.SaveChangesAsync();
		}
	}
}
