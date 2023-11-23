using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class FactoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            string expectedName = "Imperial";
            int expectedCapacity = 100;

            Factory factory = new Factory(expectedName, expectedCapacity);

            Assert.IsNotNull(factory);
            Assert.AreEqual(expectedName, factory.Name);
            Assert.AreEqual(expectedCapacity, factory.Capacity);
        }

        [Test]
        public void Constructor_SupplementsAndRobotsCollectionsAreInitialized()
        {
            string expectedName = "Imperial";
            int expectedCapacity = 100;

            Factory factory = new Factory(expectedName, expectedCapacity);

            Assert.IsNotNull(factory.Supplements);
            Assert.IsNotNull(factory.Robots);
        }

        [Test]
        public void NameAndCapacity_SettersArePublic()
        {
            string expectedName = "Union";
            int expectedCapacity = 100;
            Factory factory = new Factory("Imperial", 50);

            factory.Name = "Union";
            factory.Capacity = 100;

            Assert.AreEqual(expectedName, factory.Name);
            Assert.AreEqual(expectedCapacity, factory.Capacity);
        }

        [Test]
        public void RobotsAndSupplements_PropertiesSettersArePublic()
        {
            Factory factory = new Factory("Imperial", 50);
            Robot robot = new Robot("C3PO", 15.50, 10001);
            Supplement supplement = new Supplement("Arm", 10001);

            factory.Robots.Add(robot);
            factory.Supplements.Add(supplement);

            Assert.IsNotEmpty(factory.Robots);
            Assert.IsNotEmpty(factory.Supplements);

        }

        [Test]
        public void ProduceRobot_ReturnsUnableToProduceMessage()
        {
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);
            factory.Robots.Add(robot);

            string expectedResult = "The factory is unable to produce more robots for this production day!";

            Assert.AreEqual(expectedResult, factory.ProduceRobot("C3PO", 15.5, 10001));
        }

        [Test]
        public void ProduceRobot_ReturnsSuccessfullyProducedMessage()
        {
            string model = "C3PO";
            double price = 12.2;
            int interfaceStandard = 10001;
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);

            string expectedResult = $"Produced --> Robot model: {model} IS: {interfaceStandard}, Price: {price:f2}";

            Assert.AreEqual(expectedResult, factory.ProduceRobot(model, price, interfaceStandard));
            Assert.IsNotEmpty(factory.Robots);
            Assert.IsTrue(factory.Robots.Any(r => r.Model == model && r.Price == price && r.InterfaceStandard == interfaceStandard));
        }

        [Test]
        public void ProduceSupplement_ReturnsSuccessfullyProducesMessage()
        {
            string supplementName = "Arm";
            int supplementInterfaceStandard = 10001;
            Factory factory = new Factory("Imperial", 50);

            string expectedResult = $"Supplement: {supplementName} IS: {supplementInterfaceStandard}";

            Assert.AreEqual(expectedResult, factory.ProduceSupplement(supplementName, supplementInterfaceStandard));
            Assert.IsNotEmpty(factory.Supplements);
            Assert.IsTrue(factory.Supplements.Any(s => s.Name == supplementName && s.InterfaceStandard == supplementInterfaceStandard));
        }

        [Test]
        public void UpgradeRobot_ReturnsTrue_IfRobotDoesNotContainMatchingSupplement_SuccessfullyAddedNewSupplement()
        {
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);
            Supplement supplement = new Supplement("Arm", 10001);

            Assert.IsTrue(factory.UpgradeRobot(robot, supplement));
            Assert.IsTrue(robot.Supplements.Contains(supplement));
        }

        [Test]
        public void UpgradeRobot_ReturnsFalse_IfRobotDoesNotContainMatchingInterface()
        {
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);
            Supplement supplementExisting = new Supplement("Arm", 10001);
            Supplement supplementNew = new Supplement("Arm", 10002);

            robot.Supplements.Add(supplementExisting);

            Assert.IsFalse(factory.UpgradeRobot(robot, supplementNew));
        }

        [Test]
        public void UpgradeRobot_ReturnsFalse_IfRobotAlreadyContainsSupplement()
        {
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);
            Supplement supplementExisting = new Supplement("Arm", 10001);
            Supplement supplementNew = new Supplement("Arm", 10002);

            robot.Supplements.Add(supplementExisting);

            Assert.IsFalse(factory.UpgradeRobot(robot, supplementNew));
        }

        [Test]
        public void SellRobot_ReturnsNull()
        {
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);

            Assert.IsNull(factory.SellRobot(12.5));
        }

        [Test]
        public void SellRobot_WorksCorrectly()
        {
            Factory factory = new Factory("Imperial", 1);
            factory.ProduceRobot("C3PO", 15.50, 10001);

            Assert.IsNotNull(factory.SellRobot(18.50));
        }

        [Test]
        public void SellRobot_ReturnsCorrectRobot()
        {
            Factory factory = new Factory("Imperial", 1);
            factory.ProduceRobot("C3PO", 15.50, 10001);
            Robot robot = factory.Robots.First(r => r.Model == "C3PO");

            Assert.AreEqual(robot, factory.SellRobot(20));
            Assert.That(robot == factory.SellRobot(20));
        }

        [Test]
        public void SellRobot_ReturnsCorrectRobotWhenMoreThanOneCanBeSold()
        {
            Factory factory = new Factory("Imperial", 3);
            factory.ProduceRobot("R2D2", 16.50, 10002);
            factory.ProduceRobot("C3PO", 17.50, 10001);
            factory.ProduceRobot("Visage", 13.50, 10001);
            Robot robot = factory.Robots.FirstOrDefault(r => r.Model == "C3PO" && r.InterfaceStandard == 10001);

            Assert.AreEqual(robot, factory.SellRobot(20));
        }
    }
}