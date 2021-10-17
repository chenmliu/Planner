using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Planner.Models;

namespace Planner.ViewModels
{
	public class PeakViewModel
	{
		public PeakViewModel()
		{

		}

		public PeakViewModel(Peak peak)
		{
			Id = peak.Id;
			Name = peak.Name;
			Routes = peak.Routes.Split(";");
			TrailheadLatitude = peak.TrailheadLatitude;
			TrailheadLongitude = peak.TrailheadLongitude;
			RangerStationName = peak.RangerStationName;
			RangerStationPhone = peak.RangerStationPhone;
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

		[NotMapped]
		public IEnumerable<string> Routes
		{
			get;
			set;
		}

		public decimal TrailheadLatitude
		{
			get;
			set;
		}

		public decimal TrailheadLongitude
		{
			get;
			set;
		}

		[Display(Name = "Ranger station name")]
		public string RangerStationName
		{
			get;
			set;
		}

		[Display(Name = "Ranger station phone")]
		public string RangerStationPhone
		{
			get;
			set;
		}
	}
}
