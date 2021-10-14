using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Planner.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Planner.Models
{
    public class GroupGear
    {
		[Key]
		[Required]
		public int Id
		{
			get;
			set;
		}

		[Column("item")]
		public string Item
        {
			get;
			set;
        }

		[Required]
		[Column("trip_id")]
		public int TripId
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
