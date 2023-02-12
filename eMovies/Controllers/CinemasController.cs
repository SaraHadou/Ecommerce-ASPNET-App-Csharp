using eMovies.Data.Services;
using eMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace eMovies.Controllers
{
    public class CinemasController : Controller
    {
		private readonly ICinemasService _service;

		public CinemasController(ICinemasService service)
		{
			_service = service;
		}

		// Get: Cinemas
		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAllAsync();
			return View(data);
		}

		// Get: Cinemas/Create
		public IActionResult Create()
		{
			return View();
		}

		// Post: Cinemas/Create
		[HttpPost]
		public async Task<IActionResult> Create([Bind] Cinema cinema)
		{
			if (!ModelState.IsValid)
			{
				return View(cinema);
			}
			await _service.AddAsync(cinema);
			return RedirectToAction("Index");
		}

		// Get: Cinemas/Edit/Id
		public async Task<ActionResult> Edit(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		// Post: Cinemas/Edit/Id
		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind] Cinema cinema)
		{
			if (!ModelState.IsValid)
			{
				return View(cinema);
			}
			await _service.UpdateAsync(id, cinema);
			return RedirectToAction("Index");
		}

		// Get: Cinemas/Delete/Id
		public async Task<ActionResult> Delete(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		// Post: Cinemas/Delete/Id
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _service.DeleteAsync(id);
			return RedirectToAction("Index");
		}
	}
}
