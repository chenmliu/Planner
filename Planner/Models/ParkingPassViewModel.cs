namespace Planner.Models
{
	public class ParkingPassViewModel
	{
		public ParkingPassViewModel()
		{

		}

		public ParkingPassViewModel(ParkingPass pass)
		{
			Id = pass.Id;
			Name = pass.Name;
			ExpirationYear = ExpirationYear;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int ExpirationYear
		{
			get;
			set;
		}
	}
}
