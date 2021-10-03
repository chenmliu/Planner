using System.ComponentModel.DataAnnotations;

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
	}
}
