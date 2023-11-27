using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_InitializesProperly()
        {
            Hotel hotel = new Hotel("Rila", 5);

            Assert.IsNotNull(hotel);
            Assert.IsNotNull(hotel.Rooms);
            Assert.IsNotNull(hotel.Bookings);
            Assert.AreEqual("Rila", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        [TestCase(null)]
        public void FullName_ThrowsExceptionWhenNullOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Hotel(name, 5));
        }

        [TestCase(0)]
        [TestCase(6)]
        public void Category_ThrowsExceptionWhenLessThanOneOrMoreThanFive(int category)
        {
            Assert.Throws<ArgumentException>(() =>
                new Hotel("Rila", category));
        }

        [Test]
        public void Turnover_ReturnsCorrectValueWhenInitialized()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void AddRoom_CorrectlyAddsRoomAndRoomsCountIncreases()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            List<Room> expectedRooms = new List<Room>();
            expectedRooms.Add(room);
            expectedRooms.Add(room2);
            expectedRooms.Add(room3);

            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            Assert.AreEqual(3, hotel.Rooms.Count);
            Assert.AreEqual(expectedRooms, hotel.Rooms);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void BookRoom_ThrowsExceptionWhen_AdulstAreZeroOrNegativeNumber(int adults)
        {
            Hotel hotel = new Hotel("Rila", 5);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults, 1, 1, 100));
        }

        [Test]
        public void BookRoom_ThrowsExceptionWhen_ChildrenAreNegativeNumber()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -1, 1, 100));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void BookRoom_ThrowsExceptionWhen_ResidenceDurationIsLessThanOne(int duration)
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 1, duration, 100));
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForTwoPeople()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(2, 0, 3, 600);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(600, hotel.Turnover);
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForThreePeopleOnBigBudget()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(2, 1, 1, 600);

            Assert.AreEqual(2, hotel.Bookings.Count);
            Assert.AreEqual(1000, hotel.Turnover);
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForThreePeopleOnSmallBudget()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(2, 1, 1, 400);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(400, hotel.Turnover);
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForFivePeople()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(4, 1, 1, 600);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(600, hotel.Turnover);
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForTwoPeopleOnBigBudget()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(2, 0, 1, 600);

            Assert.AreEqual(3, hotel.Bookings.Count);
            Assert.AreEqual(1200, hotel.Turnover);
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForSixPeopleOnBigBudget()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(6, 0, 10, 10000);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(6000, hotel.Turnover);
        }

        [Test]
        public void BookRoom_BooksCorrectRoomForMultipleBookings()
        {
            Hotel hotel = new Hotel("Rila", 5);
            Room room = new Room(2, 200);
            Room room2 = new Room(4, 400);
            Room room3 = new Room(6, 600);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(2, 0, 1, 200);
            hotel.BookRoom(6, 0, 10, 10000);

            Assert.AreEqual(2, hotel.Bookings.Count);
            Assert.AreEqual(6200, hotel.Turnover);
        }

    }
}