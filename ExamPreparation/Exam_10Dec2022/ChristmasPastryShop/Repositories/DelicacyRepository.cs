using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private readonly List<IDelicacy> models;
        public DelicacyRepository()
        {
            models = new List<IDelicacy>();
        }

        public IReadOnlyCollection<IDelicacy> Models => models;
        public void AddModel(IDelicacy model)
        {
            models.Add(model);
        }
    }
}
