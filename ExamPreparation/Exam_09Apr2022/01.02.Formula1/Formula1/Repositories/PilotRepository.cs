using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> models;

        public PilotRepository()
        {
            models = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models => models;
        public void Add(IPilot model) => models.Add(model);

        public bool Remove(IPilot model) => models.Remove(model);

        public IPilot FindByName(string name) => models.FirstOrDefault(m => m.FullName == name);
    }
}
