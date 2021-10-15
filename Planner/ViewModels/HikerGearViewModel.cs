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

        public string Model
		{
            get;
            set;
		}

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
