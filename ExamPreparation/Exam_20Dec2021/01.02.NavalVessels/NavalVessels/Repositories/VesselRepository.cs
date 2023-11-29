using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private readonly List<IVessel> models;
        public VesselRepository()
        {
            models = new List<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models => models.AsReadOnly();
        public void Add(IVessel model) => models.Add(model);
        public bool Remove(IVessel model) => models.Remove(models.FirstOrDefault(m => m.Name == model.Name));
        public IVessel FindByName(string name) => models.FirstOrDefault(m => m.Name == name);
    }
}
