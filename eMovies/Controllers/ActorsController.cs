using eMovies.Data.Services;
using eMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace eMovies.Controllers
{
	public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
			_service = service;
        }

        // Get: Actors
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Actors/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind] Actor actor)
        {
			if (!ModelState.IsValid)
            {
                return View(actor);
			}
			await _service.AddAsync(actor);
			return RedirectToAction("Index");
		}

        // Get: Actors/Details/Id
        public async Task<IActionResult> Details(int id) 
        { 
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

		// Get: Actors/Edit/Id
        public async Task<ActionResult> Edit(int id)
        {
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		// Post: Actors/Edit/Id
		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind] Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return View(actor);
			}
			await _service.UpdateAsync(id, actor);
			return RedirectToAction("Index");
		}

		// Get: Actors/Delete/Id
		public async Task<ActionResult> Delete(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		// Post: Actors/Delete/Id
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _service.DeleteAsync(id);
			return RedirectToAction("Index");
		}
	}
}
