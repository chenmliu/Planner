using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public class Trip
	{
		public Trip()
		{

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

		public IEnumerable<HikerTrip> HikerTrips
		{
			get;
			set;
		}
	}
}
