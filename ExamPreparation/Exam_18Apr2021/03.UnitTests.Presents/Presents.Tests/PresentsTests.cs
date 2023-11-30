using System;

namespace Presents.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PresentsTests
    {
        [Test]
        public void Constructor_InitializesProperly()
        {
            Bag bag = new Bag();
            Assert.IsNotNull(bag);
            Assert.IsNotNull(bag.GetPresents());
            Assert.AreEqual(0, bag.GetPresents().Count);
        }

        [Test]
        public void Create_AddsPresentSuccessfully()
        {
            Bag bag = new Bag();
            Present present = new Present("Doll", 150);
            string expectedMessage = "Successfully added present Doll.";

            Assert.AreEqual(expectedMessage, bag.Create(present));
            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void Create_ThrowsExceptionWhen_PresentIsNull()
        {
            Bag bag = new Bag();
            Present present = null;

            string expectedMessage = "Present is null";

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            bag.Create(present));
            Assert.AreEqual(expectedMessage, ex.ParamName);
        }

        [Test]
        public void Create_ThrowsExceptionWhen_PresentAlreadyExists()
        {
            Bag bag = new Bag();
            Present present = new Present("Doll", 150);

            bag.Create(present);
            string expectedMessage = "This present already exists!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            bag.Create(present));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Remove_ReturnsTrueWhenPresentFound()
        {
            Bag bag = new Bag();
            Present present = new Present("Doll", 150);
            bag.Create(present);
            bag.Create(new Present("Toy", 20));

            Assert.IsTrue(bag.Remove(present));
            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void Remove_ReturnsFalseWhenPresentNotExisting()
        {
            Bag bag = new Bag();
            Present present = new Present("Doll", 150);
            bag.Create(new Present("Toy", 20));

            Assert.IsFalse(bag.Remove(present));
            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void GetPresentWithleastMagic_ReturnsCorrectResult()
        {
            Bag bag = new Bag();
            Present present = new Present("Doll", 150);
            Present present2 = new Present("Toy", 250);
            bag.Create(present);
            bag.Create(present2);

            Assert.AreEqual(present, bag.GetPresentWithLeastMagic());
        }

        [Test]
        public void GetPresentWithleastMagic_ThrowsExceptionWhen_PresentNull()
        {
            Bag bag = new Bag();

            Assert.Throws<InvalidOperationException>(() => bag.GetPresentWithLeastMagic());
        }

        [Test]
        public void GetPresent_ReturnsCorrectResult()
        {
            Bag bag = new Bag();
            Present present = new Present("Doll", 150);
            Present present2 = new Present("Toy", 250);
            bag.Create(present);
            bag.Create(present2);

            Assert.AreEqual(present, bag.GetPresent("Doll"));
        }

        [Test]
        public void GetPresent_ThrowsExceptionWhen_PresentNull()
        {
            Bag bag = new Bag();

            Assert.Throws<InvalidOperationException>(() => bag.GetPresentWithLeastMagic());
        }
    }
}
