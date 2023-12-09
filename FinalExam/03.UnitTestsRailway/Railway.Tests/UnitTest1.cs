namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    public class Tests
    {
        [Test]
        public void Constructor_InitializesProperly()
        {
            RailwayStation station = new RailwayStation("Madrid");

            Assert.IsNotNull(station);
            Assert.AreEqual("Madrid", station.Name);
        }

        [Test]
        public void Constructor_ListsInitializeProperly()
        {
            RailwayStation station = new RailwayStation("Madrid");

            Assert.AreEqual(0, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        [TestCase(null)]
        public void Name_ThrowsExceptionWhen_NullOrWhiteSpace(string name)
        {
            string expectedMessage = "Name cannot be null or empty!";

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new RailwayStation(name));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void NewArrivalOnBoard_IsWorkingProperly()
        {
            RailwayStation station = new RailwayStation("Madrid");

            station.NewArrivalOnBoard("SF20");
            station.NewArrivalOnBoard("RB30");

            Assert.AreEqual(2, station.ArrivalTrains.Count);
        }

        [Test]
        public void TrainHasArrived_WorksProperly()
        {
            RailwayStation station = new RailwayStation("Madrid");

            station.NewArrivalOnBoard("SF20");
            station.NewArrivalOnBoard("RB30");
            string expectedMessage = "SF20 is on the platform and will leave in 5 minutes.";
            string actualMessage = station.TrainHasArrived("SF20");

            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(1, station.ArrivalTrains.Count);
            Assert.AreEqual(1, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasArrived_WorksProperlyWhenOtherTrainsAreFirst()
        {
            RailwayStation station = new RailwayStation("Madrid");

            station.NewArrivalOnBoard("SF20");
            station.NewArrivalOnBoard("RB30");
            string expectedMessage = "There are other trains to arrive before RB30.";
            string actualMessage = station.TrainHasArrived("RB30");

            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(2, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasLeft_ReturnsTrue()
        {
            RailwayStation station = new RailwayStation("Madrid");

            station.NewArrivalOnBoard("SF20");
            station.NewArrivalOnBoard("RB30");
            station.TrainHasArrived("SF20");

            Assert.IsTrue(station.TrainHasLeft("SF20"));
            Assert.AreEqual(1, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasLeft_ReturnsFalse()
        {
            RailwayStation station = new RailwayStation("Madrid");

            station.NewArrivalOnBoard("SF20");
            station.NewArrivalOnBoard("RB30");
            station.TrainHasArrived("SF20");

            Assert.IsFalse(station.TrainHasLeft("RB30"));
            Assert.AreEqual(1, station.ArrivalTrains.Count);
            Assert.AreEqual(1, station.DepartureTrains.Count);
        }
    }
}