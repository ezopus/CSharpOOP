using NUnit.Framework;
using System;

namespace Robots.Tests
{
    public class RobotsTests
    {
        [Test]
        public void Constructor_InitializesProperly()
        {
            RobotManager manager = new RobotManager(20);
            Assert.IsNotNull(manager);
            Assert.AreEqual(20, manager.Capacity);
        }

        [Test]
        public void Constructor_ListIsInitialized()
        {
            RobotManager manager = new RobotManager(20);
            Assert.IsNotNull(manager.Count);
            Assert.AreEqual(0, manager.Count);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Capacity_ThrowsExceptionWhen_ValueNegative(int capacity)
        {
            string expectedMessage = "Invalid capacity!";
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new RobotManager(capacity));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Add_WorksCorrectly()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            Robot robot2 = new Robot("C3PO", 100);

            manager.Add(robot);
            manager.Add(robot2);

            Assert.AreEqual(2, manager.Count);
        }

        [Test]
        public void Add_ThrowsExceptionWhen_RobotExists()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            string expectedMessage = "There is already a robot with name R2D2!";
            manager.Add(robot);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => manager.Add(robot));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Add_ThrowsExceptionWhen_CapacityNotEnough()
        {
            RobotManager manager = new RobotManager(1);
            Robot robot = new Robot("R2D2", 50);
            Robot robot2 = new Robot("C3PO", 50);
            string expectedMessage = "Not enough capacity!";
            manager.Add(robot);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => manager.Add(robot2));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Remove_WorksCorrectly()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            Robot robot2 = new Robot("C3PO", 100);

            manager.Add(robot);
            manager.Add(robot2);

            manager.Remove("C3PO");
            Assert.AreEqual(1, manager.Count);
        }

        [Test]
        public void Remove_ThrowsExceptionWhen_RobotNullOrNotFound()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            manager.Add(robot);
            string expectedMessage = "Robot with the name C3PO doesn't exist!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => manager.Remove("C3PO"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Work_WorksCorrectly()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            manager.Add(robot);

            manager.Work("R2D2", "Clean", 10);

            Assert.AreEqual(40, robot.Battery);
        }

        [Test]
        public void Work_ThrowsExceptionWhen_RobotNullOrNotFound()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            manager.Add(robot);

            string expectedMessage = "Robot with the name C3PO doesn't exist!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => manager.Work("C3PO", "Clean", 20));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Work_ThrowsExceptionWhen_BatteryLevelNotEnough()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 10);
            manager.Add(robot);

            string expectedMessage = "R2D2 doesn't have enough battery!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => manager.Work("R2D2", "Clean", 20));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Charge_WorksCorrectly()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            manager.Add(robot);
            manager.Work("R2D2", "Clean", 30);

            manager.Charge("R2D2");
            Assert.AreEqual(50, robot.Battery);
        }

        [Test]
        public void Charge_ThrowsExceptionWhen_RobotNullOrNotFound()
        {
            RobotManager manager = new RobotManager(20);
            Robot robot = new Robot("R2D2", 50);
            manager.Add(robot);

            string expectedMessage = "Robot with the name C3PO doesn't exist!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => manager.Charge("C3PO"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

    }
}
