using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Planner.Controllers
{
	public class ResourcesController : Controller
	{
		public IActionResult Index()
		{
			// Rediect to login page if not logged in
			if (HttpContext.Session.GetString("user") == null)
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
