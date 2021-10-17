using System.ComponentModel.DataAnnotations;
using Planner.Models;

namespace Planner.ViewModels
{
    public class HikerGearViewModel
    {
        public HikerGearViewModel()
        {

        }

        public HikerGearViewModel(HikerGear hikerGear)
        {
            Id = hikerGear.Id;
            HikerId = hikerGear.HikerId;
            Item = hikerGear.Item;
            Weight = hikerGear.Weight;
            Brand = hikerGear.Brand;
            Model = hikerGear.Model;
            IntendedUse = hikerGear.IntendedUse;
            GroupUse = hikerGear.GroupUse;
        }

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

        public string Item
        {
            get;
            set;
        }

        public int Weight
        {
            get;
            set;
        }

        public string Brand
		{
            get;
            set;
		}

        public string Model
		{
            get;
            set;
		}

        [Display(Name = "Intended use")]
        public GearIntendedUse IntendedUse
        {
            get;
            set;
        }

        public bool GroupUse
        {
            get;
            set;
        }
    }
}
