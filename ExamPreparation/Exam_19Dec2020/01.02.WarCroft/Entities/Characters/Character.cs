using System;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private double health;
        private double armor;
        private Bag bag;

        protected Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = health;
            Health = health;
            BaseArmor = armor;
            Armor = armor;
            AbilityPoints = abilityPoints;
            Bag = bag;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
                name = value;
            }
        }
        public double BaseHealth { get; private set; }
        public double Health
        {
            get
            {
                if (health < 0)
                {
                    return 0;
                }

                if (health > BaseHealth)
                {
                    return BaseHealth;
                }
                return health;
            }
            set
            {
                health = value;
            }
        }
        public double BaseArmor { get; private set; }
        public double Armor
        {
            get
            {
                if (armor < 0)
                {
                    return 0;
                }
                return armor;
            }
            private set
            {
                if (value < 0)
                {
                    armor = 0;
                }
                else
                {
                    armor = value;
                }
            }
        }

        public double AbilityPoints { get; private set; }
        public Bag Bag
        {
            get => bag;
            protected set
            {
                bag = value;
            }
        }
        public bool IsAlive
        {
            get => Health > 0;
            set
            {

            }
        }
        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();
            if (Armor - hitPoints > 0)
            {
                Armor -= hitPoints;
            }
            else
            {
                hitPoints -= Armor;
                Armor = 0;
                if (Health - hitPoints > 0)
                {
                    Health -= hitPoints;
                }
                else
                {
                    IsAlive = false;
                    Health = 0;
                }
            }

        }

        public void UseItem(Item item)
        {
            EnsureAlive();
            Item usedItem = Bag.GetItem(item.GetType().Name);
            usedItem.AffectCharacter(this);
        }
    }
}