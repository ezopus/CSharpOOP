using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private int exploredPlanets = 0;
        public Controller()
        {
            astronauts = new AstronautRepository();
            planets = new PlanetRepository();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            if (type != nameof(Biologist) && type != nameof(Geodesist) && type != nameof(Meteorologist))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            IAstronaut astronaut = null;
            if (type == nameof(Biologist))
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == nameof(Geodesist))
            {
                astronaut = new Geodesist(astronautName);
            }
            else
            {
                astronaut = new Meteorologist(astronautName);
            }

            astronauts.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (string item in items)
            {
                planet.Items.Add(item);
            }

            planets.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string RetireAstronaut(string astronautName)
        {
            if (astronauts.FindByName(astronautName) == null)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            IAstronaut retiredAstronaut = astronauts.FindByName(astronautName);
            astronauts.Remove(retiredAstronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string ExplorePlanet(string planetName)
        {
            var explorers = astronauts.Models.Where(a => a.Oxygen > 60).ToList();
            if (!explorers.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet planetToExplore = planets.FindByName(planetName);

            Mission mission = new Mission();
            mission.Explore(planetToExplore, explorers);
            exploredPlanets++;
            return String.Format(OutputMessages.PlanetExplored, planetName, explorers.Count(a => !a.CanBreath));
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanets} planets were explored!");
            sb.AppendLine($"Astronauts info:");
            foreach (var a in astronauts.Models)
            {
                sb.AppendLine($"Name: {a.Name}");
                sb.AppendLine($"Oxygen: {a.Oxygen}");
                sb.Append($"Bag items: ");
                if (a.Bag.Items.Count > 0)
                {
                    sb.AppendLine(string.Join(", ", a.Bag.Items));
                }
                else
                {
                    sb.AppendLine("none");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
