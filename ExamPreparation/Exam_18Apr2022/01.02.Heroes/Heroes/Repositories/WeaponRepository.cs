using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> models;
        public WeaponRepository()
        {
            models = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => models.AsReadOnly();
        public void Add(IWeapon model) => models.Add(model);
        public bool Remove(IWeapon model) => models.Remove(models.FirstOrDefault(w => w.Name == model.Name));
        public IWeapon FindByName(string name) => models.FirstOrDefault(m => m.Name == name);
    }
}
