using WildFarm.Models.Interfaces;

namespace WildFarm.Factories.Interfaces
{
    public interface IFoodFactory
    {
        IFood CreateFood(string name, int quantity);
    }
}
