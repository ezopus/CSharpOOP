using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        protected IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);
                }
                name = value;
            }
        }

        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);
                }
                health = value;
            }
        }

        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                }
                armour = value;
            }
        }
        public IWeapon Weapon => weapon;
        public bool IsAlive
        {
            get => health > 0;
            private set
            {

            }
        }
        public void TakeDamage(int points)
        {
            if (Armour - points >= 0)
            {
                Armour -= points;
                return;
            }
            points -= armour;
            Armour = 0;
            if (Health - points > 0)
            {
                Health -= points;
                return;
            }
            Health = 0;
            IsAlive = false;
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapon = weapon;
        }
    }
}
