﻿namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private const int FreshwaterFishSize = 3;
        private const int FreshwaterSizeIncrease = 3;
        public FreshwaterFish(string name, string species, decimal price)
            : base(name, species, price)
        {
            this.Size = FreshwaterFishSize;
        }

        public override void Eat()
        {
            this.Size += FreshwaterSizeIncrease;
        }
    }
}
