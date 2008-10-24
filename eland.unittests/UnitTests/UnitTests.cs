using System.Collections.Generic;

using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private IList<IUnit> units;

        public UnitTests()
        {
            units = new List<IUnit>();
        }

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            units.Add(new Soldier());
            units.Add(new Archer());
            units.Add(new Clubman());
        }

        [Test]
        public void Check_Unit_Properties()
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
    }

    internal class Clubman : ICombatUnit, IHandToHandUnit
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

    internal class Archer : ICombatUnit, IRangedUnit
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

    internal class Soldier : ICombatUnit, IRangedUnit, IHandToHandUnit
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

    internal interface ICombatUnit : IUnit
    {
    }

    internal interface IUnit
    {
    }

}
