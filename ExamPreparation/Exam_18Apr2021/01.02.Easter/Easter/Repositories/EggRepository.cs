using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private readonly List<IEgg> models;
        public EggRepository()
        {
            models = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models => models;
        public void Add(IEgg model) => models.Add(model);
        public bool Remove(IEgg model) => models.Remove(model);
        public IEgg FindByName(string name) => models.FirstOrDefault(e => e.Name == name);
    }
}
