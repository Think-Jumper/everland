using System;
using System.Collections.Generic;
using eland.model.Interfaces;
using eland.model.Units;
using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class UpgradeTests
    {
        private readonly IList<Unit> units;

        public UpgradeTests()
        {
            units = new List<Unit>();
        }

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            units.Add(new Soldier());
            units.Add(new Archer());
            units.Add(new Clubman());
            units.Add(new Peasant());
        }

        [Test]
        public void Upgrade_Offensive_Unit_Valid_Target()
        {
            var offensiveUpgrade = new OffensiveUpgrade1();
            offensiveUpgrade.Apply(new Soldier());
        }

        [Test]
        public void Upgrade_Offensive_Unit_Verify_Properties()
        {
            var offensiveUpgrade = new OffensiveUpgrade1();
            var unit = new Soldier();
            var initialMaxHealth = unit.MaximumHealth;
            var initialAttack = unit.AttackStrength;

            offensiveUpgrade.Apply(unit);

            Assert.AreEqual(unit.MaximumHealth, initialMaxHealth * 1.25);
            Assert.AreEqual(unit.AttackStrength, initialAttack + 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Upgrade_Offensive_Unit_Invalid_Target()
        {
            var offensiveUpgrade = new OffensiveUpgrade1();
            offensiveUpgrade.Apply(new Peasant());
        }

    }
}
