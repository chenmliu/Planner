namespace Planner.Models
{
	public class HikerViewModel
	{
		public HikerViewModel()
		{

		}

		public HikerViewModel(Hiker hiker)
		{
			Id = hiker.Id;
			FirstName = hiker.FirstName;
			LastName = hiker.LastName;
			FullName = FirstName + " " + LastName;
			Phone = hiker.Phone;
			City = hiker.City;
			Awd = hiker.Awd;
			EmergencyContactName = hiker.EmergencyContactName;
			EmergencyContactPhone = hiker.EmergencyContactPhone;
			FunScale = hiker.FunScale;
		}

		public int Id
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public string FullName
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public bool Awd
		{
			get;
			set;
		}

		public string EmergencyContactName
		{
			get;
			set;
		}

		public string EmergencyContactPhone
		{
			get;
			set;
		}

		public int FunScale
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}
	}
}
