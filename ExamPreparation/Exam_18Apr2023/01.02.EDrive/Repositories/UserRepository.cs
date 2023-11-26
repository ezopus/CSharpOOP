using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> models;

        public UserRepository()
        {
            models = new List<IUser>();
        }
        public void AddModel(IUser model)
        {
            models.Add(model);
        }

        public bool RemoveById(string identifier)
        {
            return models.Remove(models.FirstOrDefault(m => m.DrivingLicenseNumber == identifier));
        }

        public IUser FindById(string identifier)
        {
            return models.FirstOrDefault(m => m.DrivingLicenseNumber == identifier);
        }

        public IReadOnlyCollection<IUser> GetAll() => models.AsReadOnly();
    }
}
