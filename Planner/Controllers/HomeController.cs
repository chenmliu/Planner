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
						{ "id", HttpContext.Session.GetString("userid") }
					}
					);
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

			var viewModel = new HikerViewModel(hiker);
			await AddTripsAsync(viewModel);
			return View(viewModel);
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

			return RedirectToAction(nameof(Details), new { id = hiker.Id });
		}

		public IActionResult SignOut()
		{
			HttpContext.Session.SetString("username", string.Empty);
			HttpContext.Session.SetInt32("userid", int.MinValue);

			var a = HttpContext.Session.GetString("username");

			return new RedirectToRouteResult(
				new RouteValueDictionary
				{
					{ "controller", "Home" },
					{ "action", "Index" }
				}
				);
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

			return View(viewModel);
		}

		private async Task<HikerViewModel> AddTripsAsync(HikerViewModel hiker)
		{
			var trips = await _dbContext.HikerTrip
				.Where(ht => ht.HikerId == hiker.Id)
				.Join(_dbContext.Trip,
						  m => m.TripId,
						  v => v.Id,
						  (m, v) => new TripViewModel() { Name = v.Name, StartDate = v.StartDate, Id = v.Id })
				.ToListAsync();
			var pastTrips = trips.Where(t => t.StartDate < DateTime.Today);
			var upcomingTrips = trips.Except(pastTrips);
			hiker.pastTrips = pastTrips;
			hiker.upcomingTrips = upcomingTrips;

			return hiker;
		}
	}
}
