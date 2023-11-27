using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private readonly List<IHotel> models;

        public HotelRepository()
        {
            models = new List<IHotel>();
        }
        public void AddNew(IHotel model)
        {
            models.Add(model);
        }

        public IHotel Select(string criteria)
        {
            return models.FirstOrDefault(h => h.FullName == criteria);
        }

        public IReadOnlyCollection<IHotel> All()
        {
            return models.AsReadOnly();
        }
    }
}
