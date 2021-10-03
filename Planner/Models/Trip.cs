﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
	}
}
