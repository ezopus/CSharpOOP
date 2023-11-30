using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const int ProfessionalDrivingExperience = 30;
        private const string ProfessionalRacingBehavior = "strict";
        public ProfessionalRacer(string username, ICar car)
            : base(username, ProfessionalRacingBehavior, ProfessionalDrivingExperience, car)
        {
        }
    }
}
