using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> models;
        public EquipmentRepository()
        {
            models = new List<IEquipment>();
        }
        public IReadOnlyCollection<IEquipment> Models => models;
        public void Add(IEquipment model) => models.Add(model);
        public bool Remove(IEquipment model) => models.Remove(model);
        public IEquipment FindByType(string type) => models.FirstOrDefault(m => m.GetType().Name == type);
    }
}
