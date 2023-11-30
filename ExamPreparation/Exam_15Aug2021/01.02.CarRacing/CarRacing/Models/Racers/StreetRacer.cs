using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const int StreetRacerDrivingExperience = 10;
        private const string StreetRacerRacingBehavior = "aggressive";
        public StreetRacer(string username, ICar car)
            : base(username, StreetRacerRacingBehavior, StreetRacerDrivingExperience, car)
        {
        }
    }
}
