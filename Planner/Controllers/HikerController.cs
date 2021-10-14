using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Planner.Models;
using Planner.ViewModels;

namespace Planner.Controllers
{
	public class HikerController : Controller
	{
		private readonly ILogger<HikerController> _logger;
		private readonly PlannerDbContext _dbContext;

		public HikerController(ILogger<HikerController> logger, PlannerDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		/// <summary>
		/// Get all the hikers.
		/// </summary>
		/// <returns>All the hikers.</returns>
		public async Task<ActionResult> Index()
		{
			// Rediect to login page if not logged in
			if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
			{
				return new RedirectToRouteResult(
					new RouteValueDictionary{
						{ "controller", "Home" },
						{ "action", "Index" }
					}
					);
			}

			var hikers = await _dbContext.Hiker
				.Select(h => new HikerViewModel(h))
				.ToListAsync()
				.ConfigureAwait(true);
			return View(hikers.OrderBy(s => s.FirstName));
		}

		/// <summary>
		/// Edit a hiker by ID.
		/// GET: Hiker/Edit/{id}
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ID of the hiker.</returns>
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			return await GetHikerViewModelByIdAsync(id);
		}

		/// <summary>
		/// Get a hiker by ID.
		/// GET: Hiker/Details/{id}
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ID of the hiker.</returns>
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			return await GetHikerViewModelByIdAsync(id);
		}

		/// <summary>
		/// Fill in the details to add a hiker.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Submit a hiker.
		/// </summary>
		/// <param name="hikerViewModel">Hiker information.</param>
		/// <returns></returns>
		[HttpPost, ActionName("Create")]
		public async Task<ActionResult> CreateSubmitted(HikerViewModel hikerViewModel)
		{
			var hiker = new Hiker(hikerViewModel);
			await _dbContext.Hiker.AddAsync(hiker).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Update a hiker by ID.
		/// </summary>
		/// <param name="updatedHiker">Updated hiker information.</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult> Edit(HikerViewModel updatedHiker)
		{
			// Retrieve the existing user's credential to re-insert these required fields.
			// For MVP, don't allow users to update credential.
			var existingHiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.Id == updatedHiker.Id)
				.ConfigureAwait(true);
			updatedHiker.UserName = existingHiker.UserName;
			updatedHiker.Password = existingHiker.Password;

			var local = _dbContext.Hiker.Local.FirstOrDefault(entry => entry.Id == updatedHiker.Id);
			if (local != null)
			{
				_dbContext.Entry(local).State = EntityState.Detached;
			}

			var hiker = new Hiker(updatedHiker);
			_dbContext.Entry(hiker).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Confirm deleting a hiker.
		/// GET: Hiker/Delete/{id}
		/// </summary>
		/// <param name="id">ID of the hiker.</param>
		/// <returns></returns>
		public async Task<IActionResult> Delete(int id)
		{
			return await GetHikerViewModelByIdAsync(id);
		}

		/// <summary>
		/// Delete a hiker by ID.
		/// POST: Hiker/Delete/{id}
		/// </summary>
		/// <param name="id">ID of the hiker.</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var hiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.Id == id)
				.ConfigureAwait(true);

			if (hiker == null)
			{
				return NotFound();
			}

			_dbContext.Hiker.Remove(hiker);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction(nameof(Index));
		}

		private async Task<IActionResult> GetHikerViewModelByIdAsync(int id)
		{
			var hiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.Id == id)
				.ConfigureAwait(true);

			if (hiker == null)
			{
				return NotFound();
			}

			var viewModel = new HikerViewModel(hiker);
			return View(viewModel);
		}
	}
}
