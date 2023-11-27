using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private readonly List<IBooking> models;
        public BookingRepository()
        {
            models = new List<IBooking>();
        }
        public void AddNew(IBooking model)
        {
            models.Add(model);
        }

        public IBooking Select(string criteria)
        {
            return models.FirstOrDefault(b => b.BookingNumber == int.Parse(criteria));
        }

        public IReadOnlyCollection<IBooking> All()
        {
            return models.AsReadOnly();
        }
    }
}