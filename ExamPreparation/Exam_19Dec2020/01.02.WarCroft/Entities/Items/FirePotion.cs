using WarCroft.Entities.Characters.Contracts;

namespace WarCroft.Entities.Items
{
    internal class FirePotion : Item
    {
        private const int FirePotionWeight = 5;
        public FirePotion()
            : base(FirePotionWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            if (character.IsAlive)
            {
                character.Health -= 20;
                if (character.Health - 20 < 0)
                {
                    character.IsAlive = false;
                }
            }
        }
    }
}
