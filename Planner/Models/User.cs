using Microsoft.AspNetCore.Mvc;

namespace Planner.Models
{
	public class User : Controller
	{
		public string UserName
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}
	}
}
