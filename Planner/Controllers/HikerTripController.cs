using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Planner.Models;
using Planner.ViewModels;

namespace Planner.Controllers
{
	public class HikerTripController : Controller
	{
		private readonly ILogger<HikerController> _logger;
		private readonly PlannerDbContext _dbContext;
		private readonly string _bearerToken;
		private readonly bool _shouldQueryBitly;

		public HikerTripController(ILogger<HikerController> logger, PlannerDbContext dbContext, IConfiguration configuration)
		{
			_logger = logger;
			_dbContext = dbContext;
			_bearerToken = configuration.GetValue<string>("BITLY_BEARER_TOKEN");
			_shouldQueryBitly = configuration.GetValue<bool>("SHOULD_QUERY_BITLY");
		}

		// <summary>
		/// Fetch the invitation link for the trip.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult> InviteViaLinkAsync(int tripId)
        {
			var request = HttpContext.Request;
			var url = request.Headers["Referer"].ToString();
			url = url.Replace("localhost", "127.0.0.1");
			var data = new Dictionary<string, string>
			{
				{ "long_url", url }
			};
			var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var shortenedLink = "https://bit.ly/3FGJfD4";
			if (_shouldQueryBitly)
            {
				var response = client.PostAsync("https://api-ssl.bitly.com/v4/shorten", content);
				var responseString = await response.Result.Content.ReadAsStringAsync();
				var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
				shortenedLink = (string) responseJson["link"];
			}

			TempData["invitationLink"] = shortenedLink;
			return RedirectToAction("Details", "Trip", new { Id = tripId });
		}

		/// <summary>
		/// Fill in the details to add a hiker.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult> Invite(int tripId)
		{
			var trip = _dbContext.Trip
				.Where(t => t.Id == tripId)
				.FirstOrDefault();

			if (trip == null) {
				return NotFound();
			}

			var allHikers = await GetAllHikersAsync();
			ViewData["allHikers"] = allHikers;

			return View(
				new HikerTripViewModel {
					TripId = trip.Id,
					TripName = trip.Name,
				}
			);
		}

		/// <summary>
		/// Accept a hiker into the trip
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult> AcceptHiker(int tripId, int hikerId, string view)
		{
			var hikerTrip = _dbContext.HikerTrip
				.Where(ht => ht.TripId == tripId)
				.Where(ht => ht.HikerId == hikerId)
				.FirstOrDefault();

			if (hikerTrip == null)
			{
				return NotFound();
			}

			hikerTrip.HikerStatus = "CONFIRMED";
			_dbContext.HikerTrip.Update(hikerTrip);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			if (view == "organizer")
			{
				return RedirectToAction("Details", "Trip", new { Id = tripId });
			}

			return RedirectToAction("Details", "Home", new { Id = hikerId });
		}

		/// <summary>
		/// Reject a hiker from joining the trip
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult> RejectHiker(int tripId, int hikerId, string view)
		{
			var hikerTrip = _dbContext.HikerTrip
				.Where(ht => ht.TripId == tripId)
				.Where(ht => ht.HikerId == hikerId)
				.FirstOrDefault();

			if (hikerTrip == null)
			{
				return NotFound();
			}

			_dbContext.HikerTrip.Remove(hikerTrip);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			if (view == "organizer")
            {
				return RedirectToAction("Details", "Trip", new { Id = tripId });
			}

			return RedirectToAction("Details", "Home", new { Id = hikerId });
		}

		/// <summary>
		/// Submit a trip.
		/// </summary>
		/// <param name="hikerTripViewModel">Information about trip and hiker connection.</param>
		/// <returns></returns>
		[HttpPost, ActionName("InviteHikerToTrip")]
		public async Task<ActionResult> InviteHikerToTrip(HikerTripViewModel hikerTripViewModel, IFormCollection form)
		{
			var hikerTrip = new HikerTrip
			{
				TripId = hikerTripViewModel.TripId,
				HikerId = int.Parse(form["hikerList"]),
				HikerStatus = "PENDING-HIKER"
			};

			await _dbContext.HikerTrip.AddAsync(hikerTrip).ConfigureAwait(true);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);
			return RedirectToAction("Details", "Trip", new { Id = hikerTripViewModel.TripId });
		}

		public ActionResult Remove(int hikerId, int tripId)
		{
			var hirekTrip = _dbContext.HikerTrip
				.Where(t => t.TripId == tripId && t.HikerId == hikerId)
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
				new HikerTripViewModel { HikerId = hiker.Id, TripId = trip.Id, TripName = trip.Name, HikerName = hiker.FullName, Hiker = hiker }
			);
		}

		[HttpPost, ActionName("Remove")]
		public async Task<ActionResult> RemoveHiker(int hikerId, int tripId)
		{

			var itemToRemove = _dbContext.HikerTrip.SingleOrDefault(ht => ht.HikerId == hikerId && ht.TripId == tripId);

			if (itemToRemove == null)
			{
				return NotFound();	
			}
			_dbContext.HikerTrip.Remove(itemToRemove);
			await _dbContext.SaveChangesAsync().ConfigureAwait(true);

			return RedirectToAction("Details", "Trip", new { Id = itemToRemove.TripId });
		}

		private async Task<IEnumerable<Hiker>> GetAllHikersAsync()
        {
			return await _dbContext.Hiker.ToListAsync();
        }
	}
}
