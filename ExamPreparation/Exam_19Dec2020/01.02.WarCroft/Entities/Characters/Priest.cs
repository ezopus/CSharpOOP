using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Priest : Character, IHealer
    {
        private const double PriestHealth = 50;
        private const double PriestArmor = 25;
        private const double PriestAbilityPoints = 40;
        private static Bag PriestBag = new Backpack();
        public Priest(string name)
            : base(name, PriestHealth, PriestArmor, PriestAbilityPoints, PriestBag)
        {
        }

        public void Heal(Character character)
        {
            if (this.IsAlive && character.IsAlive)
            {
                character.Health += this.AbilityPoints;
            }
        }
    }
}
