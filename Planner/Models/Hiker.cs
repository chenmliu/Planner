using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models
{
	public class Hiker
	{
		public Hiker()
		{

		}

		public Hiker(HikerViewModel hiker)
		{
			Id = hiker.Id;
			FirstName = hiker.FirstName;
			LastName = hiker.LastName;
			Phone = hiker.Phone;
			City = hiker.City;
			Awd = hiker.Awd;
			EmergencyContactName = hiker.EmergencyContactName;
			EmergencyContactPhone = hiker.EmergencyContactPhone;
			FunScale = FunScale;
		}

		[Key]
		[Required]
		public int Id
		{
			get;
			set;
		}

		[Column("first_name")]
		[Required]
		public string FirstName
		{
			get;
			set;
		}

		[Column("last_name")]
		[Required]
		public string LastName
		{
			get;
			set;
		}

		[Required]
		public string Phone
		{
			get;
			set;
		}

		[Required]
		public string City
		{
			get;
			set;
		}

		[Required]
		public bool Awd
		{
			get;
			set;
		}

		[Column("emergency_contact_name")]
		[Required]
		public string EmergencyContactName
		{
			get;
			set;
		}

		[Column("emergency_contact_phone")]
		[Required]
		public string EmergencyContactPhone
		{
			get;
			set;
		}

		[Column("fun_scale")]
		[Range(1, 3)]
		public int FunScale
		{
			get;
			set;
		}
	}
}
