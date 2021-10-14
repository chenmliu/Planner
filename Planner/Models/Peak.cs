using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public class Peak
	{
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

		public string Routes
		{
			get;
			set;
		}

		[Column("trailhead_latitude")]
		public decimal TrailheadLatitude
		{
			get;
			set;
		}

		[Column("trailhead_longtitude")]
		public decimal TrailheadLongitude
		{
			get;
			set;
		}
	}
}
