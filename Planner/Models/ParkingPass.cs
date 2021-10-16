using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public class ParkingPass
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

		[Required]
		[Column("expiration_year")]
		public int ExpirationYear
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

		[ForeignKey("HikerId")]
		public Hiker Hiker
		{
			get;
			set;
		}
	}
}
