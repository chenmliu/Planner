using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
    public enum GearIntendedUse
    {
		Primary,
		Extra
	}

	public enum GroupGearItems
	{
		Rope,
		Stove,
		WaterFilter,
		SatelliteMessenger,
		Tent
	}
	
    public class HikerGear
    {
		[Key]
		[Required]
		public int Id
		{
			get;
			set;
		}

		[Required]
		[Column("hiker_id")]
		public int HikerId
		{
			get;
			set;
		}

		[Required]
		[Column("item")]
		public string Item
		{
			get;
			set;
		}

		/*
		[Required]
		[Column("brand")]
		public string Brand
		{
			get;
			set;
		}

		[Required]
		[Column("model")]
		public string Model
		{
			get;
			set;
		}

		[Required]
		public GearIntendedUse IntendedUse
        { 
			get;
			set; 
		}

		[Required]
		[Column("group_use")]
		public bool GroupUse
		{ 
			get; 
			set;
		}

		*/

		[Required]
		[Column("weight")]
		public int Weight
		{
			get;
			set;
		}

		/*
		[Required]
		[Column("number_of_users")]
		// Indicates how many people can use that gear (eg. tent)
		public int NumberOfUsers
		{
			get;
			set;
		}

		[Required]
		[Column("specs")]
		public string Specs
		{
			get;
			set;
		}

		[Required]
		[Column("details")]
		public string Details
		{
			get;
			set;
		}
		*/

		[ForeignKey("HikerId")]
		public Hiker Hiker
		{
			get;
			set;
		}
	}
}
