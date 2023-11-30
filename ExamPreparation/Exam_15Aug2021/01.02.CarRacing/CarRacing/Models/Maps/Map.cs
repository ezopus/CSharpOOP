using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            double strictBehaviorMultiplier = 1.2;
            double aggressiveBehaviorMultiplier = 1.1;

            if (racerOne.IsAvailable() == false && racerTwo.IsAvailable() == false)
            {
                return string.Format(OutputMessages.RaceCannotBeCompleted);
            }

            if (racerOne.IsAvailable() == false && racerTwo.IsAvailable() == true)
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            if (racerOne.IsAvailable() == true && racerTwo.IsAvailable() == false)
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            IRacer winner;

            double racerOneChance = racerOne.Car.HorsePower * racerOne.DrivingExperience;
            double racerTwoChance = racerTwo.Car.HorsePower * racerTwo.DrivingExperience;

            if (racerOne.RacingBehavior == "strict")
            {
                racerOneChance *= strictBehaviorMultiplier;
            }
            else
            {
                racerOneChance *= aggressiveBehaviorMultiplier;
            }

            if (racerTwo.RacingBehavior == "strict")
            {
                racerTwoChance *= strictBehaviorMultiplier;
            }
            else
            {
                racerTwoChance *= aggressiveBehaviorMultiplier;
            }

            if (racerOneChance > racerTwoChance)
            {
                winner = racerOne;
            }
            else
            {
                winner = racerTwo;
            }

            racerOne.Race();
            racerTwo.Race();

            return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner.Username);
        }
    }
}
