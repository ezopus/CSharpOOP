using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private const int dummyHealth = 10;
        private const int dummyExperience = 10;
        private const int deadDummyHealth = 0;
        private Axe axe = new Axe(10, 10);

        Dummy aliveDummy = new Dummy(dummyHealth, dummyExperience);
        Dummy deadDummy = new Dummy(deadDummyHealth, dummyExperience);

        [SetUp]
        public void TestInitialize()
        {
            aliveDummy = new Dummy(dummyHealth, dummyExperience);
            deadDummy = new Dummy(deadDummyHealth, dummyExperience);
        }

        [Test]
        [TestCase(1)]
        public void Test_DummyLosesHealth_WhenAttacked(int attack)
        {
            int healthBeforeAttack = dummyHealth;
            this.aliveDummy.TakeAttack(attack);
            Assert.AreNotEqual(aliveDummy.Health, healthBeforeAttack);
        }

        [Test]
        [TestCase]
        public void Test_DeadDummy_ThrowsException_WhenAttacked()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                deadDummy.TakeAttack(axe.AttackPoints);
            }, "Dead dummy throws exception when attacked");
        }

        [Test]
        public void Test_DeadDummy_CanGiveXP()
        {
            Assert.IsNotNull(deadDummy.GiveExperience(), "Dead dummy gives XP");
        }

        [Test]
        public void Test_Dummy_CantGiveXP()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                aliveDummy.GiveExperience();
            }, "Alive dummy can't give XP");
        }
    }
}