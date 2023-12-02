using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly List<IDecoration> models;
        public DecorationRepository()
        {
            models = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => models;
        public void Add(IDecoration model) => models.Add(model);

        public bool Remove(IDecoration model) => models.Remove(model);

        public IDecoration FindByType(string type) => models.FirstOrDefault(m => m.GetType().Name == type);
    }
}
