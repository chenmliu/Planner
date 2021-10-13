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

		public async Task<ActionResult> Index()
		{
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
		public async Task<ActionResult> Login(HikerViewModel hikerViewModel)
		{
			var hiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.UserName.Equals(hikerViewModel.UserName) &&
										h.Password.Equals(hikerViewModel.Password))
				.ConfigureAwait(true);

			if (hiker == null)
			{
				return Content("Invalid user name or password.");
			}

			var viewModel = new HikerViewModel(hiker);
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

			var hiker = new Hiker(updatedHiker);
			_dbContext.Entry(hiker).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction(nameof(Details), new { id = updatedHiker.Id });
		}

		public async Task<ActionResult> SignUp()
		{
			return View();
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
