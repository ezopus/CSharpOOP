using Raiding.Models.Interfaces;

namespace Raiding.Factories.Interfaces
{
    public interface IHeroFactory
    {
        IBaseHero CreateHero(string name, string type);
    }
}
