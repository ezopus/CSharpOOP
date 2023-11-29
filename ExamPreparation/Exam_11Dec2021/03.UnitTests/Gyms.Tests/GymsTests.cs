using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    public class GymsTests
    {
        [Test]
        public void Constructor_WorksProperly()
        {
            Gym gym = new Gym("Gold", 20);

            Assert.IsNotNull(gym);
            Assert.AreEqual(20, gym.Capacity);
            Assert.AreEqual("Gold", gym.Name);
        }

        [Test]
        public void AthleteList_InitializesProperly()
        {
            Gym gym = new Gym("Gold", 20);

            Assert.IsNotNull(gym.Count);
            Assert.AreEqual(0, gym.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Name_ThrowsException_WhenNullOrEmpty(string name)
        {
            string expectedMessage = "Invalid gym name. (Parameter 'value')";

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
                new Gym(name, 20));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Capacity_ThrowsException_WhenNegative(int capacity)
        {
            string expectedMessage = "Invalid gym capacity.";

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Gym("Gold", capacity));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void AddAthlete_WorksProperly()
        {
            Gym gym = new Gym("Gold", 3);
            gym.AddAthlete(new Athlete("Gosho"));
            gym.AddAthlete(new Athlete("Ronnie"));
            gym.AddAthlete(new Athlete("Arnold"));
            Assert.AreEqual(3, gym.Count);
        }

        [Test]
        public void AddAthlete_ThrowsException_WhenGymFull()
        {
            Gym gym = new Gym("Gold", 1);
            gym.AddAthlete(new Athlete("Gosho"));

            string expectedMessage = "The gym is full.";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
               gym.AddAthlete(new Athlete("Pesho")));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void RemoveAthlete_WorksProperly()
        {
            Gym gym = new Gym("Gold", 3);
            gym.AddAthlete(new Athlete("Gosho"));
            gym.AddAthlete(new Athlete("Ronnie"));
            gym.AddAthlete(new Athlete("Arnold"));

            gym.RemoveAthlete("Ronnie");
            gym.RemoveAthlete("Arnold");
            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void RemoveAthlete_ThrowsException_AthleteNotFound()
        {
            Gym gym = new Gym("Gold", 1);
            gym.AddAthlete(new Athlete("Gosho"));

            string expectedMessage = "The athlete Arnold doesn't exist.";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
               gym.RemoveAthlete("Arnold"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }
        [Test]
        public void InjureAthlete_WorksProperly()
        {
            Gym gym = new Gym("Gold", 3);
            gym.AddAthlete(new Athlete("Gosho"));
            Athlete athlete = new Athlete("Ronnie");
            gym.AddAthlete(athlete);

            Assert.AreEqual("Ronnie", gym.InjureAthlete("Ronnie").FullName);
            Assert.IsTrue(athlete.IsInjured);
        }

        [Test]
        public void InjureAthlete_ThrowsException_AthleteNotFound()
        {
            Gym gym = new Gym("Gold", 1);
            gym.AddAthlete(new Athlete("Gosho"));

            string expectedMessage = "The athlete Arnold doesn't exist.";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
               gym.InjureAthlete("Arnold"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Report_ReturnsCorrectStringCountAndMessage()
        {
            Gym gym = new Gym("Gold", 5);
            gym.AddAthlete(new Athlete("Ronnie"));
            gym.AddAthlete(new Athlete("Arnold"));
            gym.AddAthlete(new Athlete("Jason"));

            gym.InjureAthlete("Arnold");

            string expectedMessage = $"Active athletes at Gold: Ronnie, Jason";

            Assert.AreEqual(expectedMessage, gym.Report());
        }

        [Test]
        public void Report_ReturnsCorrectStringCountAndMessageWhenNoInjuredFound()
        {
            Gym gym = new Gym("Gold", 5);
            gym.AddAthlete(new Athlete("Ronnie"));
            gym.AddAthlete(new Athlete("Arnold"));
            gym.AddAthlete(new Athlete("Jason"));

            gym.InjureAthlete("Ronnie");
            gym.InjureAthlete("Arnold");
            gym.InjureAthlete("Jason");

            string expectedMessage = $"Active athletes at Gold: ";

            Assert.AreEqual(expectedMessage, gym.Report());
        }
    }
}
