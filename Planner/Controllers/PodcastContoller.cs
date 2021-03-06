using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Planner.Controllers
{
	public class PodcastController : Controller
	{
		public IActionResult Index()
		{
			// Rediect to login page if not logged in
			if (HttpContext.Session.GetString("username") == null)
			{
				return new RedirectToRouteResult(
					new RouteValueDictionary{
						{ "controller", "Home" },
						{ "action", "Index" }
					}
					);
			}

			return View();
		}
	}
}
