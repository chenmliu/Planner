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
    }
}
