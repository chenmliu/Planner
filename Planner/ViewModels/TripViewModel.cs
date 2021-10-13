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

		public TripViewModel(Trip trip)
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

		#region TripDetails
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
		public bool BumpyRoad
		{
			get;
			set;
		}
		public int TotalDistance
		{
			get;
			set;
		}
		public bool ElevationGain
		{
			get;
			set;
		}
		#endregion

		public List<HikerTripViewModel> Hikers { get; set; }
	}
    
}
