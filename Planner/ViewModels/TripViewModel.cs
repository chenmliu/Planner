using System;
using System.Collections.Generic;
using Planner.Models;

namespace Planner.ViewModels
{
	public class TripViewModel
	{
		public TripViewModel()
		{

		}

		public TripViewModel(Trip trip, string weatherDesc = "light rain", string iconCode = "10n")
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
			Location = trip.Location;
			HasSnow = trip.HasSnow;
			NeedHighClearanceVehicle = trip.NeedHighClearanceVehicle;
			TotalDistance = trip.TotalDistance;
			WeatherLabel = weatherDesc;
			IconUrl = $"http://openweathermap.org/img/wn/{iconCode}@2x.png";
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

		public string OwnerName
		{
			get;
			set;
		}

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

		public bool HasSnow
        {
			get;
			set;
        }

		public bool NeedHighClearanceVehicle
        {
			get;
			set;
        }

		public float TotalDistance
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

		public List<HikerTripViewModel> Hikers { get; set; }
	}
    
}
