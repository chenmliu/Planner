using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public enum PermitType
    {
		DiscoverPass,
		NationalForest,
		NationalParks,
		Other
    }

    public class Permit
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

		public PermitType PermitType
		{ 
			get; 
			set;
		}

		[Required]
		[Column("expiration_date")]
		// Change type to SqlDbType.Date ?
		public DateTime ExpirationDate
		{ 
			get; 
			set;
		}
	}
}
