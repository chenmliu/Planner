using System.Collections.Generic;
using Planner.Models;

namespace Planner.ViewModels
{
	public class PeakViewModel
	{
		public PeakViewModel(Peak peak)
		{
			Id = peak.Id;
			Name = peak.Name;
			Routes = peak.Routes.Split(";");
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public IEnumerable<string> Routes
		{
			get;
			set;
		}
	}
}
