using System;
using System.Collections;
using System.Collections.Generic;

using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly IList<Unit> units;

        public UnitTests()
        {
            units = new List<Unit>();
        }

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            units.Add(new Soldier());
            units.Add(new Archer());
            units.Add(new Clubman());
        }

        [Test]
        public void Check_Unit_Interfaces()
        {

            foreach (var unit in units)
            {
                if (unit is Soldier)
                {
                    Assert.IsTrue(unit is IDefensiveUnit);
                    Assert.IsTrue(unit is IOffensiveUnit);
                    Assert.IsTrue(unit is IHandToHandUnit);
                    Assert.IsTrue(unit is IRangedUnit);
                }

                if (unit is Archer)
                {
                    Assert.IsFalse(unit is IDefensiveUnit);
                    Assert.IsTrue(unit is IOffensiveUnit);
                    Assert.IsFalse(unit is IHandToHandUnit);
                    Assert.IsTrue(unit is IRangedUnit);
                }

                if (unit is Clubman)
                {
                    Assert.IsTrue(unit is IDefensiveUnit);
                    Assert.IsTrue(unit is IOffensiveUnit);
                    Assert.IsTrue(unit is IHandToHandUnit);
                    Assert.IsFalse(unit is IRangedUnit);
                }

            }
        }

        [Test]
        public void Check_Unit_Properties()
        {
            foreach(var unit in units)
            {
                Assert.IsTrue(unit.Health >= 0);
                Assert.IsTrue(unit.Health <= unit.MaximumHealth);

                foreach(var upgrade in unit.Upgrades)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(upgrade.Name));
                }
            }
        }

    }

    internal class Clubman : Unit, IHandToHandUnit
    {
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Defend()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class Archer : Unit, IRangedUnit
    {
        public int Range
        {
            get { return 0; }
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }

    internal interface IRangedUnit : IOffensiveUnit
    {
        int Range { get; }
    }

    internal class Soldier  : Unit, IRangedUnit, IHandToHandUnit
    {
        public int Range
        {
            get { return 0; }
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Defend()
        {
            throw new System.NotImplementedException();
        }
    }

    internal interface IDefensiveUnit
    {
        void Defend();
    }

    internal interface IHandToHandUnit : IOffensiveUnit, IDefensiveUnit
    {

    }

    internal interface IOffensiveUnit
    {
        void Attack();
    }

    public abstract class Unit
    {
        protected Unit()
        {
            Health = 0;
            MaximumHealth = 0;
            Upgrades = new List<IUpgrade>();
        }

        public int Health { get; protected set; }
        public int MaximumHealth { get; protected set; }

        public IList<IUpgrade> Upgrades { get; set; }
    }

    public class IUpgrade
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}
