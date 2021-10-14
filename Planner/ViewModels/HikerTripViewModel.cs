using Planner.Models;

namespace Planner.ViewModels
{
	public class HikerTripViewModel
	{

		public int Id
		{
			get;
			set;
		}

		public int HikerId
		{
			get;
			set;
		}

		public int TripId
		{
			get;
			set;
		}
		public string HikerName
		{
			get;
			set;
		}

		public string TripName
		{
			get;
			set;
		}

		public string HikerStatus
        {
			get;
			set;
        }

		public Hiker Hiker
		{
			get;
			set;
		}
	}
}
