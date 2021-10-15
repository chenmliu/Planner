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
			HikerGroupGear = Enumerable.Empty<HikerGearViewModel>();
			pastTrips = Enumerable.Empty<TripViewModel>();
			PendingInvitations = Enumerable.Empty<TripViewModel>();
			upcomingTrips = Enumerable.Empty<TripViewModel>();
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
			HikerGroupGear = Enumerable.Empty<HikerGearViewModel>();
			pastTrips = Enumerable.Empty<TripViewModel>();
			PendingInvitations = Enumerable.Empty<TripViewModel>();
			upcomingTrips = Enumerable.Empty<TripViewModel>();
		}

		public int Id
		{
			get;
			set;
		}

		[Display(Name = "First Name")]
		public string FirstName
		{
			get;
			set;
		}

		[Display(Name = "Last Name")]
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

		[Display(Name = "Emergency Contact Name")]
		public string EmergencyContactName
		{
			get;
			set;
		}

		[Display(Name = "Emergency Contact Phone")]
		public string EmergencyContactPhone
		{
			get;
			set;
		}

		[Display(Name = "Fun Scale")]
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

		[Display(Name = "Car Brand")]
		public string CarBrand
        {
			get;
			set;
        }

		[Display(Name = "Car Model")]
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

		public bool SnowFriendly
		{
			get;
			set;
		}

		public bool HighClearance
		{
			get;
			set;
		}

		[Display(Name = "Group gear")]
		public IEnumerable<HikerGearViewModel> HikerGroupGear
        {
			get;
			set;
        }

		public IEnumerable<HikerGearViewModel> HikerExtraGear
		{
			get;
			set;
		}

		[Display(Name = "Past Trips")]
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

		[Display(Name = "Upcoming Trips")]
		public IEnumerable<TripViewModel> upcomingTrips
		{
			get;
			set;
		}
	}
}
