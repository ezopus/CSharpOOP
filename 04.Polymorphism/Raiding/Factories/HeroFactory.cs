using Raiding.Factories.Interfaces;
using Raiding.Models;
using Raiding.Models.Interfaces;

namespace Raiding.Factories
{
    public class HeroFactory : IHeroFactory
    {
        public IBaseHero CreateHero(string name, string type)
        {
            switch (type)
            {
                case "Paladin":
                    return new Paladin(name);
                case "Warrior":
                    return new Warrior(name);
                case "Rogue":
                    return new Rogue(name);
                case "Druid":
                    return new Druid(name);
                default:
                    throw new ArgumentException("Invalid hero!");
            }
        }
    }
}
