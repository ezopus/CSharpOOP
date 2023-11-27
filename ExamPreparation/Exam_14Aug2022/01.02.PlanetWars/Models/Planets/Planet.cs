using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private UnitRepository units;
        private WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            units = new UnitRepository();
            weapons = new WeaponRepository();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }
        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }

        public double MilitaryPower
        {
            get
            {
                double totalAmount = units.Models.Sum(a => a.EnduranceLevel) + weapons.Models.Sum(w => w.DestructionLevel);

                if (units.FindByName(nameof(AnonymousImpactUnit)) != null)
                {
                    totalAmount *= 1.30;
                }

                if (weapons.FindByName(nameof(NuclearWeapon)) != null)
                {
                    totalAmount *= 1.45;
                }
                return Math.Round(totalAmount, 3);
            }
        }

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;
        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;
        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public void TrainArmy()
        {
            foreach (var unit in units.Models)
            {
                unit.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            if (amount > Budget)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            if (units.Models.Count != 0)
            {
                sb.AppendLine($"--Forces: " + string.Join(", ", units.Models.Select(m => m.GetType().Name).ToList()));
            }
            else
            {
                sb.AppendLine("--Forces: No units");
            }

            if (weapons.Models.Count != 0)
            {
                sb.AppendLine($"--Combat equipment: " + string.Join(", ", weapons.Models.Select(m => m.GetType().Name).ToList()));
            }
            else
            {
                sb.AppendLine("--Combat equipment: No weapons");
            }

            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }
    }
}
