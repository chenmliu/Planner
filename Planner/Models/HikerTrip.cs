using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public class HikerTrip
	{

		[Key]
		[Required]
		public int Id
		{
			get;
			set;
		}

		[Key]
		[Required]
		[Column("hiker_id")]
		public int HikerId
		{
			get;
			set;
		}

		[Key]
		[Required]
		[Column("trip_id")]
		public int TripId
		{
			get;
			set;
		}

		[ForeignKey("HikerId")]
		public Hiker Hiker
		{
			get;
			set;
		}

		[ForeignKey("TripId")]
		public Trip Trip
		{
			get;
			set;
		}

	}
}
