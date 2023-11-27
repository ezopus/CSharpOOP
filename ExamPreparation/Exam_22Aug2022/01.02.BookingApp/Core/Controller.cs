using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotels;
        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.Select(hotelName) != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            IHotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (hotels.Select(hotelName).Rooms.Select(roomTypeName) != null)
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            if (roomTypeName != nameof(Studio) && roomTypeName != nameof(Apartment) &&
                roomTypeName != nameof(DoubleBed))
            {
                return string.Format(ExceptionMessages.RoomTypeIncorrect);
            }

            IRoom room;
            if (roomTypeName == nameof(DoubleBed))
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == nameof(Apartment))
            {
                room = new Apartment();
            }
            else
            {
                room = new Studio();
            }

            IHotel currentHotel = hotels.Select(hotelName);
            currentHotel.Rooms.AddNew(room);
            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(Studio) && roomTypeName != nameof(DoubleBed))
            {
                return string.Format(ExceptionMessages.RoomTypeIncorrect);
            }

            if (hotels.Select(hotelName).Rooms.Select(roomTypeName) == null)
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }

            IHotel currentHotel = hotels.Select(hotelName);

            if (currentHotel.Rooms.Select(roomTypeName).PricePerNight != 0)
            {
                return string.Format(ExceptionMessages.PriceAlreadySet);
            }

            currentHotel.Rooms.Select(roomTypeName).SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h => h.Category == category))
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }

            var hotelsOrdered = hotels.All().OrderBy(h => h.FullName).Select(h => h.Rooms.All().Where(r => r.PricePerNight > 0));

            var allRoomsInOneList = new List<IRoom>();
            foreach (var room in hotelsOrdered)
            {
                foreach (var r in room)
                {
                    allRoomsInOneList.Add(r);
                }
            }

            var bookedRoom = allRoomsInOneList.OrderBy(r => r.BedCapacity).Where(r => adults + children <= r.BedCapacity).FirstOrDefault();

            if (bookedRoom == null)
            {
                return string.Format(OutputMessages.RoomNotAppropriate);
            }

            IHotel currentHotel = hotels.All().FirstOrDefault(h => h.Rooms.All().Contains(bookedRoom));

            int bookingNumber = currentHotel.Bookings.All().Count + 1;
            currentHotel.Bookings.AddNew(new Booking(bookedRoom, duration, adults, children, bookingNumber));
            return string.Format(OutputMessages.BookingSuccessful, bookingNumber, currentHotel.FullName);

        }

        public string HotelReport(string hotelName)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel currentHotel = hotels.Select(hotelName);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {currentHotel.FullName}");
            sb.AppendLine($"--{currentHotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {currentHotel.Turnover:F2} $");
            sb.AppendLine($"--Bookings:");


            if (currentHotel.Bookings.All().Count == 0)
            {
                sb.AppendLine();
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in currentHotel.Bookings.All())
                {
                    sb.AppendLine();
                    sb.AppendLine(booking.BookingSummary());
                }
            }

            return sb.ToString().Trim();
        }
    }
}
