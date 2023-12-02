using System;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {
        private const double WarriorHealth = 100;
        private const double WarriorArmor = 50;
        private const double WarriorAbilityPoints = 40;
        private static Bag WarriorBag;
        public Warrior(string name)
            : base(name, WarriorHealth, WarriorArmor, WarriorAbilityPoints, WarriorBag)
        {
            Bag = new Satchel();
        }
        public void Attack(Character character)
        {
            if (this.IsAlive && character.IsAlive)
            {
                if (this.Name == character.Name)
                {
                    throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
                }

                character.TakeDamage(this.AbilityPoints);
            }
        }
    }
}
