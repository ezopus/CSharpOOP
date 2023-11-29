using Gym.Utilities.Messages;
using System;

namespace Gym.Models.Athletes
{
    public class Boxer : Athlete
    {
        private const int BoxerStamina = 60;
        private const int BoxerIncreaseStamina = 15;
        public Boxer(string fullName, string motivation, int numberOfMedals)
            : base(fullName, motivation, BoxerStamina, numberOfMedals)
        {
        }
        public override void Exercise()
        {
            if (Stamina + BoxerIncreaseStamina > 100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
            Stamina += BoxerIncreaseStamina;
        }
    }
}
