using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Planner.Models;

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
			var hikers = await _dbContext.Hiker
				.Select(h => new HikerViewModel(h))
				.ToListAsync()
				.ConfigureAwait(true);
			return View(hikers.OrderBy(s => s.FirstName));
		}

		/// <summary>
		/// Get a hiker by ID.
		/// </summary>
		/// <param name="Id"></param>
		/// <returns>ID of the hiker.</returns>
		[HttpGet]
		public async Task<ActionResult> Edit(int Id)
		{
			var hiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.Id == Id)
				.ConfigureAwait(true);
			var viewModel = new HikerViewModel(hiker);
			return View(viewModel);
		}

		/// <summary>
		/// Get a hiker by ID.
		/// </summary>
		/// <param name="Id"></param>
		/// <returns>ID of the hiker.</returns>
		[HttpGet]
		public async Task<ActionResult> Details(int Id)
		{
			var hiker = await _dbContext.Hiker
				.FirstOrDefaultAsync(h => h.Id == Id)
				.ConfigureAwait(true);
			var viewModel = new HikerViewModel(hiker);
			return View(viewModel);
		}

		/// <summary>
		/// Update a hiker by ID.
		/// </summary>
		/// <param name="updatedHiker">Updated hiker information.</param>
		/// <returns>Hiker with the given ID.</returns>
		[HttpPost]
		public async Task<ActionResult> Edit(HikerViewModel updatedHiker)
		{
			var local = _dbContext.Hiker.Local.FirstOrDefault(entry => entry.Id == updatedHiker.Id);
            if (local != null)
            {
				_dbContext.Entry(local).State = EntityState.Detached;
            }

			var hiker = new Hiker(updatedHiker);
			_dbContext.Entry(hiker).State = EntityState.Modified;
			
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			
			return RedirectToAction("Index");
		}
	}
}
