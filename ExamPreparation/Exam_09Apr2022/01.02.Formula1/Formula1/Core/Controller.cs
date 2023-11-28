using Formula1.Core.Contracts;
using Formula1.Models.Contracts;
using Formula1.Models.Models;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Linq;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilots;
        private RaceRepository races;
        private FormulaOneCarRepository cars;

        public Controller()
        {
            pilots = new PilotRepository();
            races = new RaceRepository();
            cars = new FormulaOneCarRepository();
        }

        public string CreatePilot(string fullName)
        {
            if (pilots.FindByName(fullName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            pilots.Add(new Pilot(fullName));
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (cars.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type != nameof(Ferrari) && type != nameof(Williams))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            IFormulaOneCar car;
            if (type == nameof(Ferrari))
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }
            cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (races.FindByName(raceName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            races.Add(new Race(raceName, numberOfLaps));
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            if (pilots.FindByName(pilotName) == null || pilots.FindByName(pilotName).Car != null)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            if (cars.FindByName(carModel) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage,
                    carModel));
            }

            IFormulaOneCar car = cars.FindByName(carModel);
            pilots.FindByName(pilotName).AddCar(car);
            cars.Remove(car);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            if (races.FindByName(raceName) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            IPilot pilot = pilots.FindByName(pilotFullName);
            if (pilot == null || pilot.CanRace == false || races.FindByName(raceName).Pilots.Contains(pilot))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            IRace race = races.FindByName(raceName);
            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            if (races.FindByName(raceName) == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            IRace race = races.FindByName(raceName);
            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (race.TookPlace == true)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            race.TookPlace = true;

            var topThree =
                pilots.Models
                    .OrderByDescending(p => p.Car.RaceScoreCalculator(races.FindByName(raceName).NumberOfLaps))
                    .Take(3);
            var winner = topThree.First();
            winner.WinRace();

            var second = topThree.Skip(1).First();
            var third = topThree.Last();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {winner.FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {second.FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {third.FullName} is third in the {raceName} race.");

            return sb.ToString().TrimEnd();

        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var race in races.Models.Where(r => r.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pilot in pilots.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
