﻿using Gym.Utilities.Messages;
using System;

namespace Gym.Models.Athletes
{
    public class Weightlifter : Athlete
    {
        private const int WeightlifterStamina = 50;
        private const int WeightlifterIncreaseStamina = 10;
        public Weightlifter(string fullName, string motivation, int numberOfMedals)
            : base(fullName, motivation, WeightlifterStamina, numberOfMedals)
        {
        }
        public override void Exercise()
        {
            if (Stamina + WeightlifterIncreaseStamina > 100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
            Stamina += WeightlifterIncreaseStamina;
        }
    }
}
