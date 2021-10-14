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
			FunScale = FunScale;
			UserName = hiker.UserName;

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

		[NotMapped]
		public string FullName
		{
			get {
				return $"{this.FirstName} {this.LastName}";
			}
		}

		public static string EncryptPassword(string password)
        {
			var salt = new byte[1];
			salt[0] = 0;

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
