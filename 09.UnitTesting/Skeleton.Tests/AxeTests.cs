using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private const int axeAttack = 10;
        private const int axeDurability = 10;
        private const int brokenAxeDurability = 0;
        private const int dummyHealth = 10;
        private const int dummyExperience = 10;

        Axe axe = new Axe(axeAttack, axeDurability);
        Axe brokenAxe = new Axe(axeAttack, brokenAxeDurability);
        Dummy target = new Dummy(dummyHealth, dummyExperience);

        [Test]
        [TestCase(9)]
        public void Test_CheckIfAxeLosesDurability_AfterAttack(int testNumber)
        {
            axe.Attack(target);
            Assert.AreEqual(axe.DurabilityPoints, testNumber, "Axe durability has not changed.");
        }

        [Test]
        public void Test_AttackingWithBrokenWeapon()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                brokenAxe.Attack(target);
            }, "Broken axe throws exception");
        }
    }
}