﻿using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;

namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private int damage;
        private string name;
        private int durability;

        public Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.WeaponTypeNull);
                }
                name = value;
            }
        }
        public int Durability
        {
            get => durability;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                }
                durability = value;
            }
        }

        public abstract int DoDamage();
    }
}
