using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private readonly List<Item> items;
        private readonly List<Character> characters;
        public WarController()
        {
            items = new List<Item>();
            characters = new List<Character>();
        }
        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];
            if (characterType != "Warrior" && characterType != "Priest")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }

            Character character;
            if (characterType == "Warrior")
            {
                character = new Warrior(name);
            }
            else
            {
                character = new Priest(name);
            }

            characters.Add(character);
            return string.Format(SuccessMessages.JoinParty, name);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            if (itemName != nameof(FirePotion) && itemName != nameof(HealthPotion))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }

            Item item;
            if (itemName == nameof(FirePotion))
            {
                item = new FirePotion();
            }
            else
            {
                item = new HealthPotion();
            }
            items.Add(item);
            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            Character character = characters.FirstOrDefault(c => c.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

            if (items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            Item item = items.Last();
            character.Bag.AddItem(item);
            items.Remove(item);
            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];
            Character character = characters.FirstOrDefault(c => c.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

            Item item = character.Bag.Items.FirstOrDefault(i => i.GetType().Name == itemName);
            character.UseItem(item);

            return string.Format(SuccessMessages.UsedItem, characterName, itemName);
        }

        public string GetStats()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var character in characters.OrderByDescending(c => c.IsAlive).ThenByDescending(c => c.Health))
            {
                sb.Append(
                    $"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: ");
                if (character.IsAlive)
                {
                    sb.AppendLine("Alive");
                }
                else
                {
                    sb.AppendLine("Dead");
                }
            }

            return sb.ToString().Trim();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string defenderName = args[1];
            Character attacker = characters.FirstOrDefault(c => c.Name == attackerName);
            Character defender = characters.FirstOrDefault(d => d.Name == defenderName);

            //check if attacker exists
            if (attacker == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
            }
            //check if defender exists
            if (defender == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, defenderName));
            }

            //check if attacker is alive, can attack
            if (!attacker.IsAlive)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

            //attack
            defender.TakeDamage(attacker.AbilityPoints);

            //output
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{attackerName} attacks {defenderName} for {attacker.AbilityPoints} hit points! {defenderName} has {defender.Health}/{defender.BaseHealth} HP and {defender.Armor}/{defender.BaseArmor} AP left!");
            if (!defender.IsAlive)
            {
                sb.AppendLine($"{defender.Name} is dead!");
            }

            return sb.ToString().Trim();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];

            Priest healer = characters.FirstOrDefault(c => c.Name == healerName) as Priest;
            Character healed = characters.FirstOrDefault(c => c.Name == healingReceiverName);

            //check if healer exists
            if (healer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            }
            //check if healed exists
            if (healed == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiverName));
            }

            if (!healer.IsAlive)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
            }

            healer.Heal(healed);

            return string.Format(SuccessMessages.HealCharacter, healerName, healingReceiverName, healer.AbilityPoints,
                healingReceiverName, healed.Health);
        }
    }
}
