using System;

namespace CarManager.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        [Test]
        public void Car_ConstructorsWorkCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 5.5;
            double expectedFuelCapacity = 50;
            int expectedFuelResult = 0;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelResult, car.FuelAmount);
        }

        [Test]
        public void Car_MakeSetterWorksCorrectly()
        {
            string expectedMake = "VW";

            Car car = new Car(expectedMake, "Golf", 5.5, 50);

            Assert.AreEqual(expectedMake, car.Make);
        }

        [TestCase(null)]
        public void Car_MakeShouldThrowExceptionWhenNullOrEmpty(string expectedMake)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Car(expectedMake, "Golf", 5.5, 50));

            Assert.AreEqual("Make cannot be null or empty!", ex.Message);
        }

        [Test]
        public void Car_ModelSetterWorksCorrectly()
        {
            string expectedModel = "Golf";

            Car car = new Car("VW", expectedModel, 5.5, 50);

            Assert.AreEqual(expectedModel, car.Model);
        }

        [TestCase(null)]
        public void Car_ModelShouldThrowExceptionWhenNullOrEmpty(string expectedModel)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Car("VW", expectedModel, 5.5, 50));

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Car_FuelConsumptionShouldThrowExceptionWhenZeroOrNegative(int fuelConsumption)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Car("VW", "Golf", fuelConsumption, 50));

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Car_FuelCapacityShouldThrowExceptionWhenZeroOrNegative(int fuelCapacity)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Car("VW", "Golf", 5.5, fuelCapacity));

            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void Car_RefuelWorksCorrectly()
        {
            double expectedFuelAmount = 10;
            double refuelAmount = 10;

            Car car = new Car("VW", "Golf", 5.5, 40);

            car.Refuel(refuelAmount);

            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }

        [Test]
        public void Car_RefuelWorksCorrectlyWhenRefuelValueIsMoreThanCapacity()
        {
            double expectedFuelAmount = 40;
            double refuelAmount = 100;

            Car car = new Car("VW", "Golf", 5.5, 40);

            car.Refuel(refuelAmount);

            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);

        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Car_RefuelThrowsExceptionWhenRefuelAmountIsZeroOrNegative(double refuelAmount)
        {
            Car car = new Car("VW", "Golf", 5.5, 40);

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                car.Refuel(refuelAmount));

            Assert.AreEqual("Fuel amount cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void Car_DriveWorksCorrectly()
        {
            double expectedFuel = 5;
            Car car = new Car("VW", "Golf", 5, 40);
            car.Refuel(10);

            car.Drive(100);
            Assert.AreEqual(expectedFuel, car.FuelAmount);
        }

        [Test]
        public void Car_DriveThrowsException()
        {
            Car car = new Car("VW", "Golf", 5, 40);
            car.Refuel(1);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                            car.Drive(100));
            Assert.AreEqual("You don't have enough fuel to drive!", ex.Message);
        }

    }
}