using NUnit.Framework;

namespace VehicleGarage.Tests
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
            Garage garage = new Garage(20);
            Assert.IsNotNull(garage);
        }

        [Test]
        public void Property_CapacityWorksCorrectly()
        {
            Garage garage = new Garage(20);

            Assert.AreEqual(20, garage.Capacity);

            garage.Capacity = 50;
            Assert.AreEqual(50, garage.Capacity);
        }

        [Test]
        public void Constructor_InitializesListCorrectly()
        {
            Garage garage = new Garage(20);
            Assert.IsNotNull(garage.Vehicles);
        }

        [Test]
        public void Property_VehiclesWorksCorrectly()
        {
            Garage garage = new Garage(20);
            garage.Vehicles.Add(new Vehicle("VW", "Golf", "PB3333PB"));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicle_AddsVehicleSuccessfully_ReturnsTrue()
        {
            Garage garage = new Garage(20);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");

            Assert.IsTrue(garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicle_FailsToAddVehicleWhenCountNotEnough_ReturnsFalse()
        {
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            Vehicle vehicle2 = new Vehicle("Mazda", "Miata", "CB1111PB");
            garage.AddVehicle(vehicle);

            Assert.IsFalse(garage.AddVehicle(vehicle2));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicle_FailsToAddVehicleWhenVehicleAlreadyAdded_ReturnsFalse()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            garage.AddVehicle(vehicle);

            Assert.IsFalse(garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void ChargeVehicle_SuccessfullyChargesVehicles()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            Vehicle vehicle2 = new Vehicle("Mazda", "Miata", "CB1111PB");
            Vehicle vehicle3 = new Vehicle("Opel", "Zafira", "PA2222PA");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            vehicle.BatteryLevel = 50;
            vehicle2.BatteryLevel = 80;
            vehicle3.BatteryLevel = 10;

            Assert.AreEqual(2, garage.ChargeVehicles(50));
            Assert.AreEqual(100, vehicle.BatteryLevel);
            Assert.AreEqual(100, vehicle3.BatteryLevel);
        }
        [Test]
        public void ChargeVehicle_ReturnsZeroIfNoVehiclesCharged()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            Vehicle vehicle2 = new Vehicle("Mazda", "Miata", "CB1111PB");
            Vehicle vehicle3 = new Vehicle("Opel", "Zafira", "PA2222PA");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            vehicle.BatteryLevel = 50;
            vehicle2.BatteryLevel = 80;
            vehicle3.BatteryLevel = 10;

            Assert.AreEqual(0, garage.ChargeVehicles(5));
            Assert.AreEqual(50, vehicle.BatteryLevel);
            Assert.AreEqual(80, vehicle2.BatteryLevel);
            Assert.AreEqual(10, vehicle3.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_SuccessfullyDrivesVehicle()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            Vehicle vehicle2 = new Vehicle("Mazda", "Miata", "CB1111PB");
            Vehicle vehicle3 = new Vehicle("Opel", "Zafira", "PA2222PA");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            vehicle.BatteryLevel = 50;
            vehicle2.BatteryLevel = 80;
            vehicle3.BatteryLevel = 10;

            garage.DriveVehicle("PB3333PB", 25, false);
            garage.DriveVehicle("CB1111PB", 25, false);

            Assert.AreEqual(25, vehicle.BatteryLevel);
            Assert.AreEqual(55, vehicle2.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_ChangesIsDamagedFlagIfTrue()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PB3333PB", 25, true);

            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]
        public void DriveVehicle_FailsToDriveIfCarIsDamaged()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB3333PB", 25, true);

            Assert.IsTrue(vehicle.IsDamaged);
            Assert.AreEqual(75, vehicle.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_FailsToDriveIfCarHasNotEnoughBattery()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB3333PB", 60, false);
            garage.DriveVehicle("PB3333PB", 60, false);

            Assert.AreEqual(40, vehicle.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_FailsToDriveIfBatteryDrainageIsOverHundred()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB3333PB", 101, false);

            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_FailsToDriveIfCarBatteryIsEmpty()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PB3333PB", 100, false);

            garage.DriveVehicle("PB3333PB", 20, false);

            Assert.AreEqual(0, vehicle.BatteryLevel);
        }

        [Test]
        public void RepairVehicles_SuccessfullyRepairsCountOfCars()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            Vehicle vehicle2 = new Vehicle("Mazda", "Miata", "CB1111PB");
            Vehicle vehicle3 = new Vehicle("Opel", "Zafira", "PA2222PA");

            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            garage.DriveVehicle("PB3333PB", 25, true);
            garage.DriveVehicle("CB1111PB", 25, true);
            garage.DriveVehicle("PA2222PA", 25, false);

            string expectedResult = "Vehicles repaired: 2";
            string actualResult = garage.RepairVehicles();

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsFalse(vehicle.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]
        public void RepairVehicles_RepairsZeroCars()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("VW", "Golf", "PB3333PB");
            Vehicle vehicle2 = new Vehicle("Mazda", "Miata", "CB1111PB");
            Vehicle vehicle3 = new Vehicle("Opel", "Zafira", "PA2222PA");

            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            string expectedResult = garage.RepairVehicles();

            Assert.AreEqual("Vehicles repaired: 0", expectedResult);
            Assert.IsFalse(vehicle.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
            Assert.IsFalse(vehicle3.IsDamaged);
        }

    }
}