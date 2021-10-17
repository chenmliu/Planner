using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Planner.Models;

namespace Planner.ViewModels
{
	public class TripViewModel
	{
		public TripViewModel()
		{

		}

		public TripViewModel(Trip trip, string weatherDesc, string iconCode)
		{
			Id = trip.Id;
			Name = trip.Name;
			Days = trip.Days;
			StartDate = trip.StartDate;
			PeakId = trip.PeakId;
			PeakName = trip.Peak.Name;
			OwnerId = trip.Owner.Id;
			OwnerName = trip.Owner.FirstName;
			GroupSize = trip.GroupSize;
			Hikers = new List<HikerTripViewModel>();
			GroupGearList = new List<GroupGearViewModel>();
			Location = trip.Location;
			HasSnow = trip.HasSnow;
			IsBumpyRoad = trip.IsBumpyRoad;
			NeedHighClearanceVehicle = trip.NeedHighClearanceVehicle;
			TotalDistance = trip.TotalDistance;
			ElevationGain = trip.ElevationGain;
			Permit = trip.Permit;
			var latitude = trip.Peak.TrailheadLatitude;
			var longitude = trip.Peak.TrailheadLongitude;
			WeatherLabel = weatherDesc;
			IconUrl = iconCode != "" ? $"http://openweathermap.org/img/wn/{iconCode}@2x.png" : "";
			WeatherLink = $"https://www.windy.com/{latitude}/{longitude}?{latitude},{longitude},13,i:deg0,m:eWHacOd";
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

		public int Days
		{
			get;
			set;
		}

		[Display(Name = "Start date")]
		public DateTime StartDate
		{
			get;
			set;
		}

		public int PeakId
		{
			get;
			set;
		}

		[Display(Name = "Peak")]
		public string PeakName
		{
			get;
			set;
		}

		public int OwnerId
		{
			get;
			set;
		}

		[Display(Name = "Organizer")]
		public string OwnerName
		{
			get;
			set;
		}

		[Display(Name = "Group size")]

		public int GroupSize
		{
			get;
			set;
		}

		public string Members
		{
			get;
			set;
		}

		public string Location
        {
			get;
			set;
        }

		[Display(Name = "Has snow")]
		public bool HasSnow
        {
			get;
			set;
        }

		[Display(Name = "Bumpy road")]
		public bool IsBumpyRoad
		{
			get;
			set;
		}

		[Display(Name = "Need high clearance vehicle")]
		public bool NeedHighClearanceVehicle
        {
			get;
			set;
        }

		[Display(Name = "Total distance")]
		public double TotalDistance
        {
			get;
			set;
        }

		[Display(Name = "Elevation gain")]
		public int ElevationGain
		{
			get;
			set;
		}

		public string Permit
		{
			get;
			set;
		}

		public string WeatherLabel
        {
			get;
        }

		public string IconUrl
        {
			get;
        }
		
		public string WeatherLink
        {
			get;
        }

		public List<HikerTripViewModel> Hikers { get; set; }

		public List<GroupGearViewModel> GroupGearList { get; set; }

		public IList<PotentialDriver> PotentialDrivers
		{
			get;
			set;
		}
	}
    
}
