using eMovies.Data.Services;
using eMovies.Data.Static;
using eMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMovies.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
		private readonly IProducersService _service;

		public ProducersController(IProducersService service)
		{
			_service = service;
		}

		// Get: Producers
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAllAsync();
			return View(data);
		}

		// Get: Producers/Create
		public IActionResult Create()
		{
			return View();
		}

		// Post: Producers/Create
		[HttpPost]
		public async Task<IActionResult> Create([Bind] Producer producer)
		{
			if (!ModelState.IsValid)
			{
				return View(producer);
			}
			await _service.AddAsync(producer);
			return RedirectToAction("Index");
		}

		// Get: Producers/Details/Id
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		// Get: Producers/Edit/Id
		public async Task<ActionResult> Edit(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		// Post: Producers/Edit/Id
		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind] Producer producer)
		{
			if (!ModelState.IsValid)
			{
				return View(producer);
			}
			await _service.UpdateAsync(id, producer);
			return RedirectToAction("Index");
		}

		// Get: Producers/Delete/Id
		public async Task<ActionResult> Delete(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		// Post: Producers/Delete/Id
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _service.DeleteAsync(id);
			return RedirectToAction("Index");
		}
	}
}
