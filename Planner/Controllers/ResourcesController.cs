using Microsoft.AspNetCore.Mvc;

namespace Planner.Controllers
{
	public class ResourcesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
