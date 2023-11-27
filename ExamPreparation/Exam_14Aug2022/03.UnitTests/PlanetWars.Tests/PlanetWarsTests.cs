using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
        }

        [Test]
        public void Weapon_Constructor_InitializesProperly()
        {
            Weapon weapon = new Weapon("Gun", 15.50, 5);

            Assert.IsNotNull(weapon);
            Assert.AreEqual("Gun", weapon.Name);
            Assert.AreEqual(15.50, weapon.Price);
            Assert.AreEqual(5, weapon.DestructionLevel);
        }

        [Test]
        public void Weapon_NameSetterIsPublic()
        {
            Weapon weapon = new Weapon("Gun", 15.50, 5);
            string expectedName = "Sword";

            weapon.Name = "Sword";

            Assert.AreEqual(expectedName, weapon.Name);
        }
        [Test]
        public void Weapon_DestructionLevelSetterIsPublic()
        {
            Weapon weapon = new Weapon("Gun", 15.50, 5);
            double expectedLevel = 7;

            weapon.DestructionLevel = 7;

            Assert.AreEqual(expectedLevel, weapon.DestructionLevel);
        }

        [Test]
        public void Weapon_PriceSetterIsPublic()
        {
            Weapon weapon = new Weapon("Gun", 15.50, 5);
            double expectedPrice = 2.50;

            weapon.Price = 2.50;

            Assert.AreEqual(expectedPrice, weapon.Price);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Weapon_PriceThrowsException_WhenNegative(int price)
        {
            string expectedMessage = "Price cannot be negative.";

            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Weapon("Gun", price, 5));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Weapon_IncreaseDestructionLevelWorksCorrectly()
        {
            Weapon weapon = new Weapon("Gun", 12.50, 4);

            weapon.IncreaseDestructionLevel();

            Assert.AreEqual(5, weapon.DestructionLevel);
        }

        [Test]
        public void Weapon_IsNuclearIsFalseWhenValueUnderTen()
        {
            Weapon weapon = new Weapon("Gun", 12.50, 9);

            Assert.IsFalse(weapon.IsNuclear);
        }

        [Test]
        public void Weapon_IsNuclearIsTrueWhenValueOverTen()
        {
            Weapon weapon = new Weapon("Gun", 12.50, 9);

            weapon.IncreaseDestructionLevel();
            Assert.IsTrue(weapon.IsNuclear);
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(100)]
        public void Weapon_IsNuclearIsTrueWhenValueInitiallyTenOrMore(int level)
        {
            Weapon weapon = new Weapon("Gun", 12.50, level);

            Assert.IsTrue(weapon.IsNuclear);
        }

        [Test]
        public void Planet_ConstructorInitializesProperly()
        {
            Planet planet = new Planet("Uranus", 1500);

            Assert.IsNotNull(planet);
            Assert.AreEqual("Uranus", planet.Name);
            Assert.AreEqual(1500, planet.Budget);
        }

        [Test]
        public void Planet_ConstructorInitializesWeaponsList()
        {
            Planet planet = new Planet("Uranus", 1500);

            Assert.IsNotNull(planet.Weapons);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Planet_NameSetterThrowsExceptionWhenNullOrEmpty(string name)
        {
            string expectedMessage = "Invalid planet Name";

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Planet(name, 15));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Planet_BudgetSetterThrowsExceptionWhenNegative(int budget)
        {
            string expectedMessage = "Budget cannot drop below Zero!";

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Planet("Uranus", budget));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void MilitaryPowerRatio_ReturnsCorrectValueWhenNoWeaponsAdded()
        {
            Planet planet = new Planet("Uranus", 1500);

            Assert.AreEqual(0, planet.MilitaryPowerRatio);
        }

        [Test]
        public void MilitaryPowerRatio_ReturnsCorrectValueWhenWeaponsPresent()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon = new Weapon("Gun", 100, 5);
            Weapon weapon2 = new Weapon("Sword", 100, 5);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon2);

            Assert.AreEqual(10, planet.MilitaryPowerRatio);
        }

        [Test]
        public void Profit_WorksCorreclyWhenAddedNewProfit()
        {
            Planet planet = new Planet("Uranus", 1500);

            planet.Profit(100);

            Assert.AreEqual(1600, planet.Budget);
        }

        [TestCase(1500)]
        [TestCase(100)]
        [TestCase(0)]
        public void Spend_WorksCorreclyWhenSpendingPossibleAmountLessOrEqualToBudget(double amount)
        {
            Planet planet = new Planet("Uranus", 1500);
            double expectedAmount = planet.Budget - amount;

            planet.SpendFunds(amount);

            Assert.AreEqual(expectedAmount, planet.Budget);
        }

        [TestCase(1600)]
        [TestCase(5000)]
        public void Spend_ThrowsException_WhenAmountMoreThanBudget(double amount)
        {
            Planet planet = new Planet("Uranus", 1500);
            string expectedMessage = "Not enough funds to finalize the deal.";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(amount));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Planet_AddWeapon_AddsCorrectly()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon = new Weapon("Gun", 100, 5);
            Weapon weapon2 = new Weapon("Sword", 100, 5);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon2);

            Assert.AreEqual(2, planet.Weapons.Count);
        }

        [Test]
        public void Planet_AddWeapon_ThrowsExceptionWhenSameWeaponNamePresent()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon = new Weapon("Gun", 100, 5);
            Weapon weapon2 = new Weapon("Gun", 500, 3);
            planet.AddWeapon(weapon);
            string expectedMessage = $"There is already a {weapon.Name} weapon.";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                planet.AddWeapon(weapon2));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Planet_RemoveWeapon_SuccessfullyRemovesExistingWeapon()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon = new Weapon("Gun", 100, 5);
            Weapon weapon2 = new Weapon("Sword", 500, 3);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon2);

            planet.RemoveWeapon("Gun");

            Assert.AreEqual(1, planet.Weapons.Count);
        }

        [Test]
        public void Planet_RemoveWeapon_DoesNotRemoveMissingWeapong()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon = new Weapon("Gun", 100, 5);
            Weapon weapon2 = new Weapon("Sword", 500, 3);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon2);

            planet.RemoveWeapon("Lance");

            Assert.AreEqual(2, planet.Weapons.Count);
        }

        [Test]
        public void Planet_UpgradeWeapon_IncreasesDestructionLevelOnFoundWeapon()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon = new Weapon("Gun", 100, 5);
            planet.AddWeapon(weapon);

            planet.UpgradeWeapon("Gun");

            Assert.AreEqual(6, weapon.DestructionLevel);
        }

        [Test]
        public void Planet_UpgradeWeapon_ThrowsExceptionWhenWeaponNotFound()
        {
            Planet planet = new Planet("Uranus", 1500);
            Weapon weapon2 = new Weapon("Sword", 500, 3);
            planet.AddWeapon(weapon2);

            string expectedMessage = "Gun does not exist in the weapon repository of Uranus";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                planet.UpgradeWeapon("Gun"));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void Planet_DestructOpponent_SuccessfulWin()
        {
            Planet planet = new Planet("Uranus", 1500);
            Planet planet2 = new Planet("Mars", 500);
            planet.AddWeapon(new Weapon("Ray", 150, 6));
            planet2.AddWeapon(new Weapon("Shield", 10, 1));

            string expectedMessage = "Mars is destructed!";

            Assert.AreEqual(expectedMessage, planet.DestructOpponent(planet2));
        }

        [Test]
        public void Planet_DestructOpponent_FailsAndThrowsException()
        {
            Planet planet = new Planet("Uranus", 1500);
            Planet planet2 = new Planet("Mars", 500);
            planet.AddWeapon(new Weapon("Ray", 150, 1));
            planet2.AddWeapon(new Weapon("Shield", 10, 5));

            string expectedMessage = "Mars is too strong to declare war to!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                planet.DestructOpponent(planet2));

            Assert.AreEqual(expectedMessage, ex.Message);
        }
        [Test]
        public void Planet_DestructOpponent_FailsAndThrowsException2()
        {
            Planet planet = new Planet("Uranus", 1500);
            Planet planet2 = new Planet("Mars", 500);
            planet.AddWeapon(new Weapon("Ray", 150, 5));
            planet2.AddWeapon(new Weapon("Shield", 10, 5));

            string expectedMessage = "Mars is too strong to declare war to!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                planet.DestructOpponent(planet2));

            Assert.AreEqual(expectedMessage, ex.Message);
        }
    }
}
