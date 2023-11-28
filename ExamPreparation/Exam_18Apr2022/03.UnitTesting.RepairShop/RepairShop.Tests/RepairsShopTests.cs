using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void Constructor_InitializesProperly()
            {
                Garage garage = new Garage("Parking", 50);

                Assert.IsNotNull(garage);
                Assert.AreEqual("Parking", garage.Name);
                Assert.AreEqual(50, garage.MechanicsAvailable);
            }

            [Test]
            public void Constructor_ListOfCarsInitializesProperly()
            {
                Garage garage = new Garage("Parking", 50);

                Assert.IsNotNull(garage.CarsInGarage);
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [TestCase("")]
            [TestCase(null)]
            public void Name_ThrowsException_WhenNullOrEmpty(string name)
            {
                string expectedMessage = $"Invalid garage name. (Parameter 'value')";

                ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
                    new Garage(name, 50));

                Assert.AreEqual(expectedMessage, ex.Message);
            }

            [TestCase(0)]
            [TestCase(-1)]
            public void MechanicsAvailable_ThrowsException_WhenZeroOrNegative(int mechanics)
            {
                string expectedMessage = $"At least one mechanic must work in the garage.";

                ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                    new Garage("Parking", mechanics));

                Assert.AreEqual(expectedMessage, ex.Message);
            }

            [Test]
            public void AddCar_WorksCorrectly()
            {
                Garage garage = new Garage("Parking", 50);
                Car car = new Car("VW", 3);

                garage.AddCar(car);

                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void AddCar_ThrowsException_WhenNoMechanicAvailable()
            {
                Garage garage = new Garage("Parking", 1);
                Car car = new Car("VW", 3);
                Car car2 = new Car("VW", 3);
                string expectedMessage = "No mechanic available.";

                garage.AddCar(car);

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                    garage.AddCar(car2));

                Assert.AreEqual(expectedMessage, ex.Message);
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void FixCar_WorksCorrectly()
            {
                Garage garage = new Garage("Parking", 1);
                Car car = new Car("VW", 3);

                garage.AddCar(car);

                Assert.AreEqual(car, garage.FixCar("VW"));
                Assert.AreEqual(0, car.NumberOfIssues);
            }

            [Test]
            public void FixCar_ThrowsException_WhenCarDoesNotExist()
            {
                Garage garage = new Garage("Parking", 1);
                Car car = new Car("VW", 3);
                string expectedMessage = "The car Opel doesn't exist.";

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                    garage.FixCar("Opel"));

                Assert.AreEqual(expectedMessage, ex.Message);
            }

            [Test]
            public void RemoveFixedCar_WorksCorrectly()
            {
                Garage garage = new Garage("Parking", 2);
                Car car = new Car("VW", 3);
                Car car2 = new Car("Opel", 1);
                garage.AddCar(car);
                garage.AddCar(car2);

                garage.FixCar("VW");
                garage.FixCar("Opel");

                Assert.AreEqual(2, garage.RemoveFixedCar());
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [Test]
            public void RemoveFixedCar_ThrowsException_WhenNoFixedCarsAvailable()
            {
                Garage garage = new Garage("Parking", 2);
                Car car = new Car("VW", 3);
                Car car2 = new Car("Opel", 1);
                garage.AddCar(car);
                garage.AddCar(car2);

                string expectedMessage = "No fixed cars available.";

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                    garage.RemoveFixedCar());

                Assert.AreEqual(expectedMessage, ex.Message);
            }

            [Test]
            public void Report_ReturnsCorrectNumberOfFixedCars()
            {
                Garage garage = new Garage("Parking", 2);
                Car car = new Car("VW", 3);
                Car car2 = new Car("Opel", 1);
                garage.AddCar(car);
                garage.AddCar(car2);

                string expectedMessage = "There are 2 which are not fixed: VW, Opel.";

                Assert.AreEqual(expectedMessage, garage.Report());
            }

            [Test]
            public void Report_ReturnsCorrectNumberOfFixedCars2()
            {
                Garage garage = new Garage("Parking", 2);
                Car car = new Car("VW", 3);
                Car car2 = new Car("Opel", 1);
                garage.AddCar(car);
                garage.AddCar(car2);

                garage.FixCar("VW");

                string expectedMessage = "There are 1 which are not fixed: Opel.";

                Assert.AreEqual(expectedMessage, garage.Report());
            }

            [Test]
            public void Report_ReturnsCorrectNumberOfFixedCars3()
            {
                Garage garage = new Garage("Parking", 3);
                Car car = new Car("VW", 3);
                Car car2 = new Car("Opel", 1);
                Car car3 = new Car("Mazda", 7);
                garage.AddCar(car);
                garage.AddCar(car2);
                garage.AddCar(car3);

                garage.FixCar("VW");
                garage.FixCar("Opel");
                garage.FixCar("Mazda");

                string expectedMessage = "There are 0 which are not fixed: .";

                Assert.AreEqual(expectedMessage, garage.Report());
            }
        }
    }
}