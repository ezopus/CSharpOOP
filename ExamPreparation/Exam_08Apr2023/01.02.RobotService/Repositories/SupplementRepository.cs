using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private readonly List<ISupplement> data;
        public SupplementRepository()
        {
            data = new List<ISupplement>();
        }
        public IReadOnlyCollection<ISupplement> Models()
        {
            return data.AsReadOnly();
        }

        public void AddNew(ISupplement model)
        {
            data.Add(model);
        }

        public bool RemoveByName(string typeName)
        {
            return data.Remove(data.FirstOrDefault(d => d.GetType().Name == typeName));
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return data.FirstOrDefault(d => d.InterfaceStandard == interfaceStandard);
        }
    }
}
