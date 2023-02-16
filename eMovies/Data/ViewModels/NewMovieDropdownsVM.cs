using eMovies.Models;

namespace eMovies.Data.ViewModels
{
	public class NewMovieDropdownsVM
	{
		public NewMovieDropdownsVM()
		{
			Actors = new List<Actor>();
			Producers = new List<Producer>();
			Cinemas = new List<Cinema>();
		}

		public List<Producer> Producers { get; set; }
		public List<Cinema> Cinemas { get; set; }
		public List<Actor> Actors { get; set; }
	}
}
