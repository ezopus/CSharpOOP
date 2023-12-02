using NUnit.Framework;
using System;

namespace Aquariums.Tests
{
    public class AquariumsTests
    {
        [Test]
        public void Constructor_InitializesProperly()
        {
            Aquarium aquarium = new Aquarium("Underworld", 20);
            Assert.IsNotNull(aquarium);
            Assert.AreEqual("Underworld", aquarium.Name);
            Assert.AreEqual(20, aquarium.Capacity);
        }

        [Test]
        public void Constructor_List_InitializesProperly()
        {
            Aquarium aquarium = new Aquarium("Underworld", 20);
            Assert.AreEqual(0, aquarium.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Name_ThrowsExceptionWhen_NullOrEmpty(string name)
        {
            string expectedMessage = "Invalid aquarium name! (Parameter 'value')";
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
                new Aquarium(name, 20));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Capacity_ThrowsExceptionWhen_Negative(int capacity)
        {
            string expectedMessage = "Invalid aquarium capacity!";
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Aquarium("Nemo", capacity));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Add_WorksProperly()
        {
            Aquarium aquarium = new Aquarium("Nemo", 5);
            Fish fish = new Fish("Nemo");
            Fish fish2 = new Fish("Dory");
            Fish fish3 = new Fish("Albon");

            aquarium.Add(fish);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            Assert.AreEqual(3, aquarium.Count);
        }

        [Test]
        public void Add_ThrowsExceptionWhen_AquariumIsFull()
        {
            Aquarium aquarium = new Aquarium("Nemo", 1);
            Fish fish = new Fish("Nemo");
            Fish fish2 = new Fish("Dory");
            string expectedMessage = "Aquarium is full!";

            aquarium.Add(fish);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                aquarium.Add(fish2));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Remove_WorksProperly()
        {
            Aquarium aquarium = new Aquarium("Nemo", 5);
            Fish fish = new Fish("Nemo");
            Fish fish2 = new Fish("Dory");
            Fish fish3 = new Fish("Albon");

            aquarium.Add(fish);
            aquarium.Add(fish2);
            aquarium.Add(fish3);
            aquarium.RemoveFish("Nemo");
            aquarium.RemoveFish("Dory");

            Assert.AreEqual(1, aquarium.Count);
        }

        [Test]
        public void Remove_ThrowsExceptionWhen_FishNotFound()
        {
            Aquarium aquarium = new Aquarium("Nemo", 1);
            Fish fish = new Fish("Nemo");
            Fish fish2 = new Fish("Dory");
            string expectedMessage = "Fish with the name Albon doesn't exist!";

            aquarium.Add(fish);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                aquarium.RemoveFish("Albon"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void SellFish_WorksProperly()
        {
            Aquarium aquarium = new Aquarium("Nemo", 5);
            Fish fish = new Fish("Nemo");
            Fish fish2 = new Fish("Dory");

            aquarium.Add(fish);
            aquarium.Add(fish2);

            Fish soldFish = aquarium.SellFish("Nemo");

            Assert.AreEqual(fish, soldFish);
            Assert.IsFalse(soldFish.Available);
        }

        [Test]
        public void SellFish_ThrowsExceptionWhen_FishNotFound()
        {
            Aquarium aquarium = new Aquarium("Nemo", 1);
            string expectedMessage = "Fish with the name Albon doesn't exist!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                aquarium.SellFish("Albon"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Report_WorksCorrectly()
        {
            Aquarium aquarium = new Aquarium("Nemo", 5);
            Fish fish = new Fish("Nemo");
            Fish fish2 = new Fish("Dory");
            Fish fish3 = new Fish("Albon");
            aquarium.Add(fish);
            aquarium.Add(fish2);
            aquarium.Add(fish3);
            string expectedMessage = "Fish available at Nemo: Nemo, Dory, Albon";

            Assert.AreEqual(expectedMessage, aquarium.Report());
        }
    }
}
