﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Planner.ViewModels;

namespace Planner.Models
{
	public class Trip
	{
		public Trip()
		{

		}

		public Trip(TripViewModel trip)
		{
			Id = trip.Id;
			Name = trip.Name;
			Days = trip.Days;
			StartDate = trip.StartDate;
			PeakId = trip.PeakId;
			OwnerId = trip.OwnerId;
			GroupSize = trip.GroupSize;
			ElevationGain = trip.ElevationGain;
			TotalDistance = trip.TotalDistance;
			HasSnow = trip.HasSnow;
			IsBumpyRoad = trip.IsBumpyRoad;
			Location = trip.Location;
			NeedHighClearanceVehicle = trip.NeedHighClearanceVehicle;
		}

		[Key]
		[Required]
		public int Id
		{
			get;
			set;
		}

		[Required]
		public string Name
		{
			get;
			set;
		}

		[Required]
		[Column("days")]
		public int Days
		{
			get;
			set;
		}

		[Column("start_date")]
		[Required]
		public DateTime StartDate
		{
			get;
			set;
		}

		[Column("peak_id")]
		[Required]
		public int PeakId
		{
			get;
			set;
		}

		[ForeignKey("PeakId")]
		public Peak Peak
		{
			get;
			set;
		}

		[Column("owner_id")]
		[Required]
		public int OwnerId
		{
			get;
			set;
		}

		[ForeignKey("OwnerId")]
		public Hiker Owner
		{
			get;
			set;
		}

		[Column("group_size")]
		[Required]
		public int GroupSize
		{
			get;
			set;
		}

		public IEnumerable<HikerTrip> HikerTrips
		{
			get;
			set;
		}

		public IEnumerable<GroupGear> GroupGear
		{
			get;
			set;
		}

		[Column("location")]
		public string Location
		{
			get;
			set;
		}

		[Column("hasSnow")]
		public bool HasSnow
		{
			get;
			set;
		}

		[Column("needHighClearanceVehicle")]
		public bool NeedHighClearanceVehicle
		{
			get;
			set;
		}

		[Column("isBumpyRoad")]
		public bool IsBumpyRoad
		{
			get;
			set;
		}

		[Column("totalDistance")]
		public double TotalDistance
		{
			get;
			set;
		}

		[Column("elevationGain")]
		public int ElevationGain
		{
			get;
			set;
		}
	}
}
