using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class CocktailRepository : IRepository<ICocktail>
    {
        private readonly List<ICocktail> models;
        public CocktailRepository()
        {
            models = new List<ICocktail>();
        }

        public IReadOnlyCollection<ICocktail> Models => models;
        public void AddModel(ICocktail model)
        {
            models.Add(model);
        }
    }
}
