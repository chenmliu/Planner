﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		[Display(Name = "Spaces")]
		public int? Spaces
        {
			get;
			set;
        }

		[Display(Name = "Gear")]
		public IEnumerable<HikerGearViewModel> HikerGearList
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

		[Display(Name = "Upcoming Trips")]
		public IEnumerable<TripViewModel> upcomingTrips
		{
			get;
			set;
		}
	}
}
