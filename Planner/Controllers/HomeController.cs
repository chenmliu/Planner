using System;
using System.Collections.Generic;
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
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly PlannerDbContext _dbContext;

		public HomeController(ILogger<HomeController> logger, PlannerDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		public ActionResult Index()
		{
			if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
			{
				return new RedirectToRouteResult(
					new RouteValueDictionary{
						{ "controller", "Home" },
						{ "action", "Details" },
						{ "id", HttpContext.Session.GetInt32("userid") }
					});
			}
			
			return View();
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
			if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("username")))
			{
				return new RedirectToRouteResult(
					new RouteValueDictionary{
						{ "controller", "Home" },
						{ "action", "Index" },
					});
			}

			return await GetHikerViewModelByIdAsync(id);
		}

		/// <summary>
		/// Login and validate username and password.
		/// </summary>
		/// <param name="hikerViewModel">Hiker information.</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult> LoginSubmitted(HikerViewModel hikerViewModel)
		{
			var hiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.UserName.Equals(hikerViewModel.UserName) &&
										h.Password.Equals(Hiker.EncryptPassword(hikerViewModel.Password)))
				.ConfigureAwait(true);

			if (hiker == null)
			{
				return Content("Invalid user name or password.");
			}

			HttpContext.Session.SetString("username", hiker.UserName);
			HttpContext.Session.SetInt32("userid", hiker.Id);

			return new RedirectToRouteResult(
					new RouteValueDictionary{
						{ "controller", "Home" },
						{ "action", "Details" },
						{ "id", hiker.Id }
					});
		}

		[HttpPost]
		public async Task<ActionResult> AddGroupGear(string item, string brand, string model, int hikerId)
		{
			var gear = new HikerGear()
			{
				HikerId = hikerId,
				Item = item,
				Brand = brand,
				Model = model,
				IntendedUse = GearIntendedUse.Primary,
				GroupUse = true,
				Weight = 1,
				NumberOfUsers = 1,
				Specs = "specs",
				Details = "details"
			};
			await _dbContext.HikerGear.AddAsync(gear).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return new RedirectToRouteResult(
				new RouteValueDictionary
				{
					{ "controller", "Home" },
					{ "action", "Details" },
					{ "id", hikerId }
			});
		}

		[HttpPost]
		public async Task<ActionResult> RemoveGear(string item, string brand, string model, int hikerId)
		{
			var gear = await _dbContext.HikerGear
				.FirstOrDefaultAsync(g => g.Item.Equals(item)
					&& g.Brand.Equals(brand)
					&& g.Model.Equals(model)
					&& g.HikerId.Equals(hikerId))
				.ConfigureAwait(true);

			if (gear == null)
			{
				return NotFound();
			}

			_dbContext.HikerGear.Remove(gear);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return new RedirectToRouteResult(
				new RouteValueDictionary
				{
					{ "controller", "Home" },
					{ "action", "Details" },
					{ "id", hikerId }
			});
		}

		[HttpPost]
		public async Task<ActionResult> AddExtraGear(string item, string brand, string model, int hikerId)
		{
			var gear = new HikerGear()
			{
				HikerId = hikerId,
				Item = item,
				Brand = brand,
				Model = model,
				IntendedUse = GearIntendedUse.Extra,
				GroupUse = false,
				Weight = 1,
				NumberOfUsers = 1,
				Specs = "specs",
				Details = "details"
			};
			await _dbContext.HikerGear.AddAsync(gear).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return new RedirectToRouteResult(
				new RouteValueDictionary
				{
					{ "controller", "Home" },
					{ "action", "Details" },
					{ "id", hikerId }
			});
		}

		/// <summary>
		/// Edit a hiker by ID.
		/// GET: Home/Edit/{id}
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ID of the hiker.</returns>
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			return await GetHikerViewModelByIdAsync(id);
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

			var hiker = new Hiker(updatedHiker, existingHiker.Password);
			_dbContext.Entry(hiker).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction(nameof(Details), new { id = updatedHiker.Id });
		}

		public ActionResult SignUp()
		{
			return View();
		}

		public ActionResult Login()
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
			var existingHiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.UserName.Equals(hikerViewModel.UserName))
				.ConfigureAwait(true);

			if (existingHiker != null)
			{
				return Content("User already exist");
			}

			var hiker = new Hiker(hikerViewModel);
			await _dbContext.Hiker.AddAsync(hiker).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			// Log in with the newly created user
			HttpContext.Session.SetString("username", hiker.UserName);
			HttpContext.Session.SetInt32("userid", hiker.Id);

			return RedirectToAction(nameof(Details), new { id = hiker.Id });
		}

		public IActionResult SignOut()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index"); 
		}

		[HttpPost]
		public async Task<ActionResult> AddParkingPass(string name, int expirationYear, int hikerId)
		{
			var pass = new ParkingPass()
			{
				Name = name,
				ExpirationYear = expirationYear,
				HikerId = hikerId
			};
			await _dbContext.ParkingPass.AddAsync(pass).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction("Edit", new { Id = hikerId });
		}

		[HttpPost]
		public async Task<ActionResult> RemoveParkingPass(string name, int expirationYear, int hikerId)
		{
			var pass = await _dbContext.ParkingPass
				.FirstOrDefaultAsync(g => g.Name.Equals(name)
					&& g.ExpirationYear == expirationYear
					&& g.HikerId == hikerId)
				.ConfigureAwait(true);

			if (pass == null)
			{
				return NotFound();
			}

			_dbContext.ParkingPass.Remove(pass);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction("Edit", new { Id = hikerId });
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
			await AddTripsAsync(viewModel);
			await AddPendingTripInvitations(viewModel);

			var hikerGear = await _dbContext.HikerGear
				.Where(hg => hg.HikerId == hiker.Id)
				.Select(h => new HikerGearViewModel(h))
				.ToListAsync();
			viewModel.HikerGroupGear = hikerGear.Where(g => g.GroupUse == true).ToList();
			viewModel.HikerExtraGear = hikerGear.Where(g => g.IntendedUse == GearIntendedUse.Extra).ToList();

			var parkingPasses = await _dbContext.ParkingPass
				.Where(p => p.HikerId == hiker.Id)
				.Select(p => new ParkingPassViewModel(p))
				.ToListAsync();
			viewModel.ParkingPasses = parkingPasses;

			return View(viewModel);
		}

		private async Task<HikerViewModel> AddPendingTripInvitations(HikerViewModel hiker)
        {
			var pendingInvitations = await _dbContext.HikerTrip
				.Where(ht => ht.HikerId == hiker.Id)
				.Where(ht => ht.HikerStatus == "PENDING-HIKER")
				.Join(_dbContext.Trip,
						m => m.TripId,
						v => v.Id,
						(m, v) => new TripViewModel() { Name = v.Name, OwnerName = v.Owner.FullName, Id = v.Id })
				.ToListAsync();

			hiker.PendingInvitations = pendingInvitations;
			return hiker;
		}

		private async Task<HikerViewModel> AddTripsAsync(HikerViewModel hiker)
		{
			var trips = await _dbContext.HikerTrip
				.Where(ht => ht.HikerId == hiker.Id)
				.Join(_dbContext.Trip,
						  m => m.TripId,
						  v => v.Id,
						  (m, v) => new TripViewModel() { Name = v.Name, StartDate = v.StartDate, Id = v.Id, OwnerName = v.Owner.FullName })
				.ToListAsync();
			var pastTrips = trips.Where(t => t.StartDate < DateTime.Today);
			var upcomingTrips = trips.Except(pastTrips);
			hiker.pastTrips = pastTrips;
			hiker.upcomingTrips = upcomingTrips;

			return hiker;
		}
	}
}
