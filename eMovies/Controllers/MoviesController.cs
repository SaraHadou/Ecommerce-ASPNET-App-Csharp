using eMovies.Data.Services;
using eMovies.Data.ViewModels;
using eMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace eMovies.Controllers
{
    public class MoviesController : Controller
    {
		private readonly IMoviesService _service;

		public MoviesController(IMoviesService service)
		{
			_service = service;
		}

		// Get: Movies
		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAllAsync();
			return View(data);
		}

		// Filter
		public async Task<IActionResult> Filter(string searchString)
		{
			var allMovies = await _service.GetAllAsync();
			if (!searchString.IsNullOrEmpty())
			{
				var filteredMovies = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase)||
														  string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
				return View("Index", filteredMovies);
			}
			return View(allMovies);
		}

		// Get: Movies/Create
		public async Task<IActionResult> Create()
		{
			var movieDropdownsDb = await _service.GetNewMovieDropdownsValues();
			ViewBag.Cinemas = new SelectList(movieDropdownsDb.Cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(movieDropdownsDb.Producers, "Id", "FullName");
			ViewBag.Actors = new SelectList(movieDropdownsDb.Actors, "Id", "FullName");
			return View();
		}

		// Post: Movies/Create
		[HttpPost]
		public async Task<IActionResult> Create([Bind] NewMovieVM movie)
		{
			if (!ModelState.IsValid)
			{
				var movieDropdownsDb = await _service.GetNewMovieDropdownsValues();
				ViewBag.Cinemas = new SelectList(movieDropdownsDb.Cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(movieDropdownsDb.Producers, "Id", "FullName");
				ViewBag.Actors = new SelectList(movieDropdownsDb.Actors, "Id", "FullName");
				return View(movie);
			}
			await _service.AddNewMovieAsync(movie);
			return RedirectToAction("Index");
		}

		// Get: Movies/Details/Id
		public async Task<IActionResult> Details(int id)
		{
			var movieDetails = await _service.GetMovieByIdAsync(id);
			if (movieDetails == null) return View("NotFound");
			return View(movieDetails);
		}

		// Get: Movies/Edit/Id
		public async Task<ActionResult> Edit(int id)
		{
			var movieDetails = await _service.GetMovieByIdAsync(id);
			if (movieDetails == null) return View("NotFound");

			var response = new NewMovieVM()
			{
				Id = movieDetails.Id,
				Name = movieDetails.Name,
				Description = movieDetails.Description,
				Price = movieDetails.Price,
				StartDate = movieDetails.StartDate,
				EndDate = movieDetails.EndDate,
				ImageURL = movieDetails.ImageURL,
				MovieCategory = movieDetails.MovieCategory,
				CinemaId = movieDetails.CinemaId,
				ProducerId = movieDetails.ProducerId,
				ActorIds = movieDetails.Actors_Movies.Select(a => a.ActorId).ToList()	
			};

			var movieDropdownsDb = await _service.GetNewMovieDropdownsValues();
			ViewBag.Cinemas = new SelectList(movieDropdownsDb.Cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(movieDropdownsDb.Producers, "Id", "FullName");
			ViewBag.Actors = new SelectList(movieDropdownsDb.Actors, "Id", "FullName");

			return View(response);
		}

		// Post: Movies/Edit/Id
		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind] NewMovieVM movie)
		{
			if (id != movie.Id) return View("NotFound");
			if (!ModelState.IsValid)
			{
				var movieDropdownsDb = await _service.GetNewMovieDropdownsValues();
				ViewBag.Cinemas = new SelectList(movieDropdownsDb.Cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(movieDropdownsDb.Producers, "Id", "FullName");
				ViewBag.Actors = new SelectList(movieDropdownsDb.Actors, "Id", "FullName");
				return View(movie);
			}
			await _service.UpdateMovieAsync(movie);
			return RedirectToAction("Index");
		}

	}
}
