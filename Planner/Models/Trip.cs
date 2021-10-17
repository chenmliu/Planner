using System;
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
			Permit = trip.Permit;
			RiverCrossing = trip.RiverCrossing;
			Glacier = trip.Glacier;
			RockClimb = trip.RockClimb;
			Overnight = trip.Overnight;
			PotentialAvalanche = trip.PotentialAvalanche;
			MeetingTime = trip.MeetingTime;
			MeetingLocation = trip.MeetingLocation;
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

		[Column("permit")]
		public string Permit
		{
			get;
			set;
		}

		[Column("river_crossing")]
		public bool RiverCrossing
		{
			get;
			set;
		}

		public bool Glacier
		{
			get;
			set;
		}

		[Column("rock_climb")]
		public bool RockClimb
		{
			get;
			set;
		}

		public bool Overnight
		{
			get;
			set;
		}

		[Column("potential_avalanche")]
		public bool PotentialAvalanche
		{
			get;
			set;
		}

		[Column("meeting_time")]
		public bool MeetingTime
		{
			get;
			set;
		}

		[Column("meeting_location")]
		public bool MeetingLocation
		{
			get;
			set;
		}
	}
}
