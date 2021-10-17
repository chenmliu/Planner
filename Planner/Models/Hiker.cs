using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Planner.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Planner.Models
{
	public class Hiker
	{
		public Hiker()
		{

		}

		public Hiker(HikerViewModel hiker, string encryptedPassword = null)
		{
			Id = hiker.Id;
			FirstName = hiker.FirstName;
			LastName = hiker.LastName;
			Phone = hiker.Phone;
			City = hiker.City;
			Awd = hiker.Awd;
			EmergencyContactName = hiker.EmergencyContactName;
			EmergencyContactPhone = hiker.EmergencyContactPhone;
			FunScale = hiker.FunScale;
			UserName = hiker.UserName;
			HasCar = hiker.HasCar;
			CarBrand = hiker.CarBrand;
			CarModel = hiker.CarModel;
			Spaces = hiker.Spaces;
			Preference = hiker.Preference;
			SnowFriendly = hiker.SnowFriendly;
			HighClearance = hiker.HighClearance;
			LicensePlate = hiker.LicensePlate;

			if (encryptedPassword == null)
            {
				Password = EncryptPassword(hiker.Password);
			}
			else
            {
				Password = encryptedPassword;
            }
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

		public IEnumerable<HikerTrip> HikerTrips
		{
			get;
			set;
		}

		[Column("user_name")]
		[Required]
		public string UserName
		{
			get;
			set;
		}

		[Required]
		public string Password
		{
			get;
			set;
		}

		[Column("has_car")]
		[Required]
		public bool HasCar
		{
			get;
			set;
		}

		[Column("car_brand")]
		public string CarBrand
		{
			get;
			set;
		}

		[Column("car_model")]
		public string CarModel
		{
			get;
			set;
		}

		[Column("spaces")]
		public int? Spaces
		{
			get;
			set;
		}

        [Column("preference")]
        public CarpoolPreference Preference
        {
            get;
            set;
        }

        [Column("snow_friendly")]
        public bool SnowFriendly
        {
            get;
            set;
        }

        [Column("high_clearance")]
        public bool HighClearance
        {
            get;
            set;
        }

        [NotMapped]
		public List<HikerGear> HikerGear
        {
			get;
			set;
        }

		[NotMapped]

		public string FullName
		{
			get {
				return $"{this.FirstName} {this.LastName}";
			}
		}

		[Column("license_plate")]
		public string LicensePlate
		{
			get;
			set;
		}

		public static string EncryptPassword(string password)
        {
			var salt = new byte[1];
			salt[0] = 0;

			// handling NullException for password
			if (password == null)
            {
				return null;
            }

			// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
			// TODO: Add salt management
			return Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));
		}
	}
}
