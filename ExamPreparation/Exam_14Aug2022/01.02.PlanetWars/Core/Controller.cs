using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;
        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string CreatePlanet(string name, double budget)
        {
            if (planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            IPlanet planet = new Planet(name, budget);
            planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (unitTypeName != nameof(StormTroopers) && unitTypeName != nameof(SpaceForces) &&
                unitTypeName != nameof(AnonymousImpactUnit))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            IPlanet planet = planets.FindByName(planetName);
            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit;
            if (unitTypeName == nameof(StormTroopers))
            {
                unit = new StormTroopers();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else
            {
                unit = new AnonymousImpactUnit();
            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (weaponTypeName != nameof(BioChemicalWeapon) && weaponTypeName != nameof(NuclearWeapon) &&
                weaponTypeName != nameof(SpaceMissiles))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IPlanet planet = planets.FindByName(planetName);
            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon;
            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new SpaceMissiles(destructionLevel);
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet currentPlanet = planets.FindByName(planetName);
            if (currentPlanet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (!currentPlanet.Army.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

            currentPlanet.Spend(1.25d);
            currentPlanet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet attacker = planets.FindByName(planetOne);
            IPlanet defender = planets.FindByName(planetTwo);
            IPlanet winner = null;
            IPlanet loser = null;
            //attacker wins on military power
            if (attacker.MilitaryPower > defender.MilitaryPower)
            {
                winner = attacker;
                loser = defender;
            }
            //defender wins on military power
            else if (attacker.MilitaryPower < defender.MilitaryPower)
            {
                winner = defender;
                loser = attacker;
            }

            //attacker and defender have same military power
            else
            {
                var firstHasNuclear = attacker.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon));
                var secondHasNuclear = defender.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon));

                if ((firstHasNuclear && secondHasNuclear) || (!firstHasNuclear && !secondHasNuclear))
                {
                    attacker.Spend(attacker.Budget / 2);
                    defender.Spend(defender.Budget / 2);
                    return string.Format(OutputMessages.NoWinner);
                }

                if (firstHasNuclear)
                {
                    winner = attacker;
                    loser = defender;
                }
                else
                {
                    winner = defender;
                    loser = attacker;
                }
            }
            loser.Spend(loser.Budget / 2);
            winner.Spend(winner.Budget / 2);
            winner.Profit(loser.Budget);
            double totalProfit = loser.Weapons.Sum(w => w.Price) + loser.Army.Sum(u => u.Cost);
            winner.Profit(totalProfit);
            planets.RemoveItem(loser.Name);

            return string.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in planets.Models.OrderByDescending(m => m.MilitaryPower).ThenBy(n => n.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
