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
			Members = trip.Members;
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


		public List<HikerTripViewModel> Hikers { get; set; }
	}
    
}
