using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private readonly List<IVehicle> models;

        public VehicleRepository()
        {
            models = new List<IVehicle>();
        }
        public void AddModel(IVehicle model)
        {
            models.Add(model);
        }

        public bool RemoveById(string identifier)
        {
            return models.Remove(models.FirstOrDefault(m => m.LicensePlateNumber == identifier));
        }

        public IVehicle FindById(string identifier)
        {
            return models.FirstOrDefault(m => m.LicensePlateNumber == identifier);
        }

        public IReadOnlyCollection<IVehicle> GetAll() => models.AsReadOnly();
    }
}
