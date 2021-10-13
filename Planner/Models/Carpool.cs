using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public enum CarpoolPreference
    {
		Alone,
		Driver,
		Rider,
		Either
    }

	public class Carpool
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

		[Required]
		[Column("car_brand")]
		public string CarBrand
		{
			get;
			set;
		}

		[Required]
		[Column("car_model")]
		public string CarModel
		{
			get;
			set;
		}

		[Required]
		[Column("spaces")]
		// Indicates how many people can fit in the car (including driver)
		public int Spaces
		{
			get;
			set;
		}

		public CarpoolPreference Preference
		{ 
			get; 
			set;
		}

		[Required]
		[Column("awd")]
		public bool Awd
		{
			get;
			set;
		}

		[Required]
		[Column("snow_friendly")]
		public bool SnowFriendly
		{
			get;
			set;
		}

		[Required]
		[Column("high_clearance")]
		public bool HighClearance
		{
			get;
			set;
		}
	}
}
