using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }
        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
            }

            if (type != nameof(Barbarian) && type != nameof(Knight))
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
            }

            if (type == nameof(Knight))
            {
                heroes.Add(new Knight(name, health, armour));
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }

            heroes.Add(new Barbarian(name, health, armour));
            return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
        }
        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            if (type != nameof(Mace) && type != nameof(Claymore))
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            }

            IWeapon weapon;
            if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else
            {
                weapon = new Claymore(name, durability);
            }

            weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }


        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (heroes.FindByName(heroName) == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            if (weapons.FindByName(weaponName) == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (heroes.FindByName(heroName).Weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }

            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string StartBattle()
        {
            Map map = new Map();
            return map.Fight(heroes.Models.ToList().AsReadOnly());
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var hero in heroes.Models.OrderBy(h => h.GetType().Name).ThenByDescending(h => h.Health).ThenBy(h => h.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                if (hero.Weapon != null)
                {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}");
                }
                else
                {
                    sb.AppendLine($"--Weapon: Unarmed");
                }

            }
            return sb.ToString().TrimEnd();
        }
    }
}
