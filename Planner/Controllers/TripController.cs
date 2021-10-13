using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Planner.Models;
using Planner.ViewModels;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.Contrib.EntityFramework;

namespace Planner.Controllers
{
	public class TripController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly PlannerDbContext _dbContext;

		public TripController(ILogger<HomeController> logger, PlannerDbContext dbContext)
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
		/// GET: Trip/Edit/{id}
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ID of the trip.</returns>
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			return await GetTripViewModelByIdAsync(id);
		}

		/// <summary>
		/// Get a trip by ID.
		/// GET: Trip/Details/{id}
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ID of the trip.</returns>
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			return await GetTripViewModelByIdAsync(id);
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

			return RedirectToAction("Details", new { Id = trip.Id });
		}

		/// <summary>
		/// Update a trip by ID.
		/// </summary>
		/// <param name="updatedTrip">Updated trip information.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Confirm deleting a trip.
		/// GET: Trip/Delete/{id}
		/// </summary>
		/// <param name="id">ID of the trip.</param>
		/// <returns></returns>
		public async Task<IActionResult> Delete(int id)
		{
			return await GetTripViewModelByIdAsync(id);
		}

		/// <summary>
		/// Delete a trip by ID.
		/// POST: Trip/Delete/{id}
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

			if (trip == null)
			{
				return NotFound();
			}

			_dbContext.Trip.Remove(trip);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction(nameof(Index));
		}

		private async Task<IActionResult> GetTripViewModelByIdAsync(int id)
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

			var hikers = await _dbContext.HikerTrip
				.Where(ht => ht.TripId == trip.Id)
				//.Include(ht => ht.Hiker)
				.Join(_dbContext.Hiker,
						  m => m.HikerId,
						  v => v.Id,
						  (m, v) => new HikerTripViewModel() { HikerId=v.Id, TripId=m.TripId, HikerName = v.FullName   })
				.ToListAsync();

			// FIXME: Remove this hard-coded coord for Mount Rainier.
            // Should query for weather based on representative lat/lon per day.
            // NWAC can use the lat/long for Day 1.
			var coord = new Point(new Position(46.879967, -121.726906));

			var weather = await GetWeatherForecast(coord: coord);
			// FIXME: Re-enable and troubleshoot.
			//var nwacZone = GetNWACZone(coord: coord);

			// TODO: Hook weather and nwacZone up to TripViewModel.
			var viewModel = new TripViewModel(trip);
			viewModel.Hikers = hikers;
			return View(viewModel);
		}

		/// <summary>
        /// Returns the weather forecast as a JSON string.
        /// </summary>
        /// <param name="coord">The coordinate object to query.</param>
        /// <returns>Weather forecast as JSON string.</returns>
		private async Task<string> GetWeatherForecast(Point coord)
        {
			// TODO: How to retrieve this from appsettings?
			// var api_key = this.Configuration.GetValue<string>("WEATHER_API_KEY");
			var api_key = "";
			if (api_key == "")
            {
				return "";
            }
			var url = $"https://api.openweathermap.org/data/2.5/onecall?lat={coord.Coordinates.Latitude}&lon={coord.Coordinates.Longitude}&exclude=hourly,minutely,current,alerts&appid={api_key}";

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, url);

			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode(); // Throw an exception if error

			var body = await response.Content.ReadAsStringAsync();
			return body;
		}

		private NWACZone? GetNWACZone(Point coord)
		{
			// TODO: Load zone file. Iterate through each zone and check if
			// given coord is in the zone. If so, return that zone.

			// FIXME: Remove this hardcoding when feasible and load the polygons
			// from `nwac-zones.geojson`.
			Polygon westSlopesSouth = new Polygon(new List<LineString>
			{
				new LineString(new List<Position>
				{
					new Position(47.35089751723473, -121.69687292605553),
					new Position(47.28372003118022, -121.48863827242204),
					new Position(47.18913767299202, -121.27465921455024),
					new Position(47.09047557691994, -121.32492275163429),
					new Position(46.94066835672754, -121.403908309909),
					new Position(46.92007377841517, -121.43406643215914),
					new Position(46.89162062733354, -121.4556079480525),
					new Position(46.84351029424147, -121.45991625123091),
					new Position(46.718615197716645, -121.34502788385201),
					new Position(46.633876181421044, -121.29045604358959),
					new Position(46.56480445684494, -121.34502788385201),
					new Position(46.3947113292017, -121.32923077219698),
					new Position(46.26654581713993, -121.4039077476741),
					new Position(46.18209430487835, -121.4182687582692),
					new Position(45.88998907099992, -121.51017922608006),
					new Position(45.80997747862074, -121.84565122294195),
					new Position(45.78795067958177, -122.068246887171),
					new Position(45.814982354943794, -122.16590175922016),
					new Position(45.85300471698295, -122.21616529630373),
					new Position(46.20994036254817, -122.32243677471013),
					new Position(46.339146614840416, -122.31965175666089),
					new Position(46.7186470764417, -122.05838461739975),
					new Position(47.029851493706275, -121.92769942098153),
					new Position(47.21746370725208, -121.74674987678083),
					new Position(47.35089751723473, -121.69687292605553)
				})
			});
			var zones = new List<Polygon> { westSlopesSouth };

			foreach (Polygon zone in zones)
            {
				// TODO: is the coord inside the zone?
				var geometry = zone.ToDbGeometry();
				var checkPoint = coord.ToDbGeometry();
				if (checkPoint.Intersects(geometry))
				{
					// FIXME: Since we only have WSC hard-coded, this must
					// be what we return. We would need to inspect the label
					// for the zone.
					return NWACZone.WestSlopesCentral;
                };
            }
			return null;
        }
	}
}
