using WarCroft.Entities.Characters.Contracts;

namespace WarCroft.Entities.Items
{
    public class HealthPotion : Item
    {
        private const int HealthPotionWeight = 5;
        public HealthPotion()
            : base(HealthPotionWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            if (character.IsAlive)
            {
                character.Health += 20;
            }
        }
    }
}
