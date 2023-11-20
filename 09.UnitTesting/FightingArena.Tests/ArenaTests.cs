using System;
using System.Linq;

namespace FightingArena.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [Test]
        public void ArenaIsCorrectlyInitialized()
        {
            Assert.IsNotNull(arena);
            Assert.IsNotNull(arena.Warriors);
        }

        [Test]
        public void ArenaCountIsWorkingCorrectly()
        {
            Warrior warrior = new("Gosho", 10, 20);
            int expectedResult = 1;

            arena.Enroll(warrior);

            Assert.AreEqual(expectedResult, arena.Count);
        }

        [Test]
        public void ArenaEnrollIsWorkingCorrectly()
        {
            Warrior warrior = new("Gosho", 10, 20);

            int expectedResult = 1;

            arena.Enroll(warrior);

            Assert.IsNotEmpty(arena.Warriors);
            Assert.AreEqual(warrior, arena.Warriors.Single());
        }

        [Test]
        public void ArenaEnrollShouldThrowExceptionIfWarriorIsEnrolled()
        {
            Warrior warrior = new("Gosho", 10, 20);

            arena.Enroll(warrior);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior));

            Assert.AreEqual("Warrior is already enrolled for the fights!", exception.Message);
        }

        [Test]
        public void ArenaFight_ShouldWorkCorrectly()
        {
            Warrior attacker = new Warrior("Gosho", 50, 100);
            Warrior defender = new Warrior("Pesho", 30, 80);

            int expectedAttackerHP = 70;
            int expectedDefenderHP = 30;
            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);
            Assert.AreEqual(expectedAttackerHP, attacker.HP);
            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }

        [Test]
        public void ArenaFight_ShouldThrowExceptionWhen_AttackerNull()
        {
            Warrior attacker = new Warrior("Gosho", 50, 100);
            Warrior defender = new Warrior("Pesho", 30, 80);

            arena.Enroll(defender);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Fight(attacker.Name, defender.Name));

            Assert.AreEqual($"There is no fighter with name {attacker.Name} enrolled for the fights!", exception.Message);
        }

        [Test]
        public void ArenaFight_ShouldThrowExceptionWhen_DefenderNull()
        {
            Warrior attacker = new Warrior("Gosho", 50, 100);
            Warrior defender = new Warrior("Pesho", 30, 80);

            arena.Enroll(attacker);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Fight(attacker.Name, defender.Name));

            Assert.AreEqual($"There is no fighter with name {defender.Name} enrolled for the fights!", exception.Message);
        }

    }
}
