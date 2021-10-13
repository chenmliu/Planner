using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Planner.Models;
using Planner.ViewModels;

namespace Planner.Controllers
{
	public class HikerTripController : Controller
	{
		private readonly ILogger<HikerController> _logger;
		private readonly PlannerDbContext _dbContext;

		public HikerTripController(ILogger<HikerController> logger, PlannerDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		/// <summary>
		/// Fill in the details to add a hiker.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Create(int tripId)
		{
			var trip = _dbContext.Trip
				.Where(t => t.Id == tripId)
				.FirstOrDefault();
			
			if (trip == null) {
				return NotFound();
			}

			return View(
				new HikerTripViewModel {
					TripId = trip.Id,
					TripName= trip.Name 
				}
			);
		}

		/// <summary>
		/// Submit a trip.
		/// </summary>
		/// <param name="hikerTripViewModel">Information about trip and hiker connection.</param>
		/// <returns></returns>
		[HttpPost, ActionName("Create")]
		public async Task<ActionResult> CreateSubmitted(HikerTripViewModel hikerTripViewModel)
		{
            //var hikerTrip = new HikerTrip
            //{
            //	Trip = await _dbContext.Trip.FindAsync(hikerTripViewModel.TripId),
            //	Hiker = await _dbContext.Hiker.FindAsync(hikerTripViewModel.HikerId)
            //};
            var hikerTrip = new HikerTrip
			{
                TripId = hikerTripViewModel.TripId,
                HikerId = hikerTripViewModel.HikerId
            };

            await _dbContext.HikerTrip.AddAsync(hikerTrip).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction("Details", "Trip", new { Id = hikerTripViewModel.TripId });
		}

		public ActionResult Remove(int hikerId, int tripId)
		{
			var hirekTrip = _dbContext.HikerTrip
				.Where(t => t.TripId == tripId && t.HikerId == hikerId)
				//.Include(t => t.Hiker)
				//.Include(t => t.Trip)
				//.Select(t => )
				.FirstOrDefault();
			if (hirekTrip == null)
            {
				return NotFound();
            }

			var hiker = _dbContext.Hiker
				.Where(t => t.Id == hikerId)
				.FirstOrDefault();
			var trip = _dbContext.Trip
				.Where(t => t.Id == tripId)
				.FirstOrDefault();
		
			return View(
				new HikerTripViewModel { HikerId = hiker.Id, TripId = trip.Id, TripName = trip.Name, HikerName = hiker.FullName }
			);
		}



	}
}
