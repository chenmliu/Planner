﻿using System;
using System.Collections.Generic;
using Planner.Models;

namespace Planner.ViewModels
{
    public class GroupGearViewModel
    {
        public GroupGearViewModel()
        {

        }

        public GroupGearViewModel(GroupGear groupGear)
        {
            Id = groupGear.Id;
            TripId = groupGear.TripId;
            Item = groupGear.Item;
        }

        public int Id
        {
            get;
            set;
        }

        public int TripId
        {
            get;
            set;
        }

        public string Item
        {
            get;
            set;
        }
    }
}