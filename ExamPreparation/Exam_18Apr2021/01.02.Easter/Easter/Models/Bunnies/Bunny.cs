﻿using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private readonly List<IDye> dyes;

        public Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            dyes = new List<IDye>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyType);
                }
                name = value;
            }
        }
        public int Energy
        {
            get => energy;
            protected set
            {
                if (energy < 0)
                {
                    energy = 0;
                }
                else
                {
                    energy = value;
                }
            }
        }
        public ICollection<IDye> Dyes => dyes;
        public abstract void Work();

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }
    }
}
