using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        private readonly List<IRoom> models;

        public RoomRepository()
        {
            models = new List<IRoom>();
        }
        public void AddNew(IRoom model)
        {
            models.Add(model);
        }

        public IRoom Select(string criteria)
        {
            return models.FirstOrDefault(r => r.GetType().Name == criteria);
        }

        public IReadOnlyCollection<IRoom> All()
        {
            return models.AsReadOnly();
        }
    }
}
