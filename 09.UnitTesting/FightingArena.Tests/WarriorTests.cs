using System;

namespace FightingArena.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void Warrior_ConstructorInitializedCorrectly()
        {
            string expectedName = "Gosho";
            int expectedDamage = 25;
            int expectedHP = 50;

            Warrior warrior = new Warrior(expectedName, expectedDamage, expectedHP);

            Assert.IsNotNull(warrior);
            Assert.AreEqual(expectedName, warrior.Name);
            Assert.AreEqual(expectedDamage, warrior.Damage);
            Assert.AreEqual(expectedHP, warrior.HP);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        [TestCase(null)]
        public void Warrior_NameThrowsExceptionWhen_IsNullOrWhitespace(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => new Warrior(name, 25, 50));

            Assert.AreEqual("Name should not be empty or whitespace!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Warrior_DamageValueShouldBePositive(int damage)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => new Warrior("Pesho", damage, 50));

            Assert.AreEqual("Damage value should be positive!", exception.Message);
        }

        [TestCase(-1)]
        [TestCase(-20)]
        public void Warrior_HPValueShouldBePositive(int hp)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => new Warrior("Pesho", 50, hp));

            Assert.AreEqual("HP should not be negative!", exception.Message);
        }

        [Test]
        public void WarriorAttack_WorksCorrectly()
        {
            Warrior attacker = new Warrior("Pesho", 50, 100);
            Warrior defender = new Warrior("Gosho", 20, 100);

            int expectedAttackerHP = 80;
            int expectedDefenderHP = 50;

            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHP, attacker.HP);
            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }

        [TestCase(30)]
        [TestCase(29)]
        public void WarriorAttack_ThrowsExceptionWhen_AttackerHasLessAttackThan30(int hp)
        {
            Warrior attacker = new Warrior("Pesho", 30, hp);
            Warrior defender = new Warrior("Gosho", 20, 100);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", ex.Message);
        }

        [TestCase(30)]
        [TestCase(29)]
        public void WarriorAttack_ThrowsExceptionWhen_DefenderHasLessAttackThan30(int hp)
        {
            Warrior attacker = new Warrior("Pesho", 30, 100);
            Warrior defender = new Warrior("Gosho", 20, hp);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));

            Assert.AreEqual("Enemy HP must be greater than 30 in order to attack him!", ex.Message);
        }

        [Test]
        public void WarriorAttack_ThrowsExceptionWhen_AttackerHasLessAttackThanDefenderHP()
        {
            Warrior attacker = new Warrior("Pesho", 30, 40);
            Warrior defender = new Warrior("Gosho", 50, 100);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));

            Assert.AreEqual("You are trying to attack too strong enemy", ex.Message);
        }

        [Test]
        public void WarriorAttack_DefenderLosesAllHP()
        {
            Warrior attacker = new Warrior("Pesho", 50, 50);
            Warrior defender = new Warrior("Gosho", 40, 40);

            attacker.Attack(defender);

            Assert.AreEqual(0, defender.HP);
        }
    }
}