using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Planner.Models;

namespace Planner.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly PlannerDbContext _dbContext;

		public HomeController(ILogger<HomeController> logger, PlannerDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		/// <summary>
		/// Get all the trips.
		/// </summary>
		/// <returns>All the trips.</returns>
		public async Task<ActionResult> Index()
		{
			var trips = await _dbContext.Trip
				.Include(t => t.Peak)
				.Include(t => t.Owner)
				.Select(t => new TripViewModel(t))
				.ToListAsync()
				.ConfigureAwait(true);
			return View(trips.OrderBy(t => t.Name));
		}

		/// <summary>
		/// Get a trip by ID.
		/// </summary>
		/// <param name="Id"></param>
		/// <returns>ID of the trip.</returns>
		[HttpGet]
		public async Task<ActionResult> Edit(int Id)
		{
			var trip = await _dbContext.Trip
				.Include(t => t.Peak)
				.Include(t => t.Owner)
				.FirstOrDefaultAsync(t => t.Id == Id)
				.ConfigureAwait(true);
			var viewModel = new TripViewModel(trip);
			return View(viewModel);
		}

		/// <summary>
		/// Get a trip by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ID of the trip.</returns>
		[HttpGet]
		public async Task<ActionResult> Details(int id)
		{
			var trip = await _dbContext.Trip
				.Include(t => t.Peak)
				.Include(t => t.Owner)
				.FirstOrDefaultAsync(t => t.Id == id)
				.ConfigureAwait(true);
			var viewModel = new TripViewModel(trip);
			return View(viewModel);
		}

		/// <summary>
		/// Fill in the details to add a trip.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Submit a trip.
		/// </summary>
		/// <param name="tripViewModel">Trip information.</param>
		/// <returns></returns>
		[HttpPost, ActionName("Create")]
		public async Task<ActionResult> CreateSubmitted(TripViewModel tripViewModel)
		{
			var trip = new Trip(tripViewModel);
			await _dbContext.Trip.AddAsync(trip).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Update a trip by ID.
		/// </summary>
		/// <param name="updatedTrip">Updated trip information.</param>
		/// <returns>Trip with the given ID.</returns>
		[HttpPost]
		public async Task<ActionResult> Edit(TripViewModel updatedTrip)
		{
			var local = _dbContext.Trip.Local.FirstOrDefault(entry => entry.Id == updatedTrip.Id);
			if (local != null)
			{
				_dbContext.Entry(local).State = EntityState.Detached;
			}

			var trip = new Trip(updatedTrip);
			_dbContext.Entry(trip).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction(nameof(Index));
		}

		// GET: Trip/Delete/{id}
		/// <summary>
		/// Confirm deleting a trip.
		/// </summary>
		/// <param name="id">ID of the trip.</param>
		/// <returns></returns>
		public async Task<IActionResult> Delete(int id)
		{
			var trip = await _dbContext.Trip
				.Include(t => t.Peak)
				.Include(t => t.Owner)
				.FirstOrDefaultAsync(t => t.Id == id)
				.ConfigureAwait(true);
			if (trip == null)
			{
				return NotFound();
			}

			var viewModel = new TripViewModel(trip);
			return View(viewModel);
		}

		// POST: Trip/Delete/{id}
		/// <summary>
		/// Delete a trip by ID.
		/// </summary>
		/// <param name="id">ID of the trip.</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var trip = await _dbContext.Trip
				.Include(t => t.Peak)
				.Include(t => t.Owner)
				.FirstOrDefaultAsync(t => t.Id == id)
				.ConfigureAwait(true);
			_dbContext.Trip.Remove(trip);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction(nameof(Index));
		}
	}
}
