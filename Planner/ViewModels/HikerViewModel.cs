using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Planner.Models;

namespace Planner.ViewModels
{
	public class HikerViewModel
	{
		public HikerViewModel()
		{
			HikerGroupGear = new List<HikerGearViewModel>();
			pastTrips = new List<TripViewModel>();
			PendingInvitations = new List<TripViewModel>();
			upcomingTrips = new List<TripViewModel>();
			ParkingPasses = new List<ParkingPassViewModel>();
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
			HasCar = hiker.HasCar;
			CarModelBrand = hiker.CarBrand + " " + hiker.CarModel;
			CarBrand = hiker.CarBrand;
			CarModel = hiker.CarModel;
			Spaces = hiker.Spaces;
			Preference = hiker.Preference;
			SnowFriendly = hiker.SnowFriendly;
			HighClearance = hiker.HighClearance;
			HikerGroupGear = new List<HikerGearViewModel>();
			pastTrips = new List<TripViewModel>();
			PendingInvitations = new List<TripViewModel>();
			upcomingTrips = new List<TripViewModel>();
			ParkingPasses = new List<ParkingPassViewModel>();
		}

		public int Id
		{
			get;
			set;
		}

		[Display(Name = "First name")]
		public string FirstName
		{
			get;
			set;
		}

		[Display(Name = "Last name")]
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

		[Display(Name = "Phone")]
		public string Phone
		{
			get;
			set;
		}

		[Display(Name = "City")]
		public string City
		{
			get;
			set;
		}

		[Display(Name = "Awd")]
		public bool Awd
		{
			get;
			set;
		}

		[Display(Name = "Emergency contact name")]
		public string EmergencyContactName
		{
			get;
			set;
		}

		[Display(Name = "Emergency contact phone")]
		public string EmergencyContactPhone
		{
			get;
			set;
		}

		[Display(Name = "Fun scale")]
		public int FunScale
		{
			get;
			set;
		}

		[Display(Name = "Username")]
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

		[Display(Name = "Has a car")]
		public bool HasCar
        {
			get;
			set;
        }

		[Display(Name = "Car brand")]
		public string CarBrand
        {
			get;
			set;
        }

		[Display(Name = "Car model")]
		public string CarModel
		{
			get;
			set;
		}

		public string CarModelBrand
		{
			get;
			set;
		}

		[Display(Name = "Spaces")]
		public int? Spaces
        {
			get;
			set;
        }

		public CarpoolPreference Preference
		{
			get;
			set;
		}

		[Display(Name = "Snow friendly")]
		public bool SnowFriendly
		{
			get;
			set;
		}

		[Display(Name = "High clearance")]
		public bool HighClearance
		{
			get;
			set;
		}

		[Display(Name = "Group gear")]
		public IList<HikerGearViewModel> HikerGroupGear
        {
			get;
			set;
        }

		public IList<HikerGearViewModel> HikerExtraGear
		{
			get;
			set;
		}

		public IList<ParkingPassViewModel> ParkingPasses
		{
			get;
			set;
		}

		[Display(Name = "Past trips")]
		public IEnumerable<TripViewModel> pastTrips
		{
			get;
			set;
		}

		public IEnumerable<TripViewModel> PendingInvitations
        {
			get;
			set;
        }

		[Display(Name = "Upcoming trips")]
		public IEnumerable<TripViewModel> upcomingTrips
		{
			get;
			set;
		}
	}
}
