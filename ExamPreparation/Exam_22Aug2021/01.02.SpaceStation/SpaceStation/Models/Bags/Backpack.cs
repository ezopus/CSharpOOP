﻿using SpaceStation.Models.Bags.Contracts;
using System.Collections.Generic;

namespace SpaceStation.Models.Bags
{
    public class Backpack : IBag
    {
        private readonly List<string> items;
        public Backpack()
        {
            items = new List<string>();
        }
        public ICollection<string> Items => items;
    }
}
