using System.ComponentModel.DataAnnotations;

namespace Planner.Models
{
	public class PredefinedGroupGear
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
	}
}
