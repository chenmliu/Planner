using System.Collections.Generic;
using Planner.Models;

namespace Planner.ViewModels
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
			HasCar = hiker.HasCar;
			CarBrand = hiker.CarBrand;
			CarModel = hiker.CarModel;
			Spaces = hiker.Spaces;
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

		public bool HasCar
        {
			get;
			set;
        }

		public string CarBrand
        {
			get;
			set;
        }
		public string CarModel
		{
			get;
			set;
		}

		public int? Spaces
        {
			get;
			set;
        }

		public IEnumerable<HikerGearViewModel> HikerGearList
        {
			get;
			set;
        }

		public IEnumerable<TripViewModel> pastTrips
		{
			get;
			set;
		}

		public IEnumerable<TripViewModel> upcomingTrips
		{
			get;
			set;
		}
	}
}
