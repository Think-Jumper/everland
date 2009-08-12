﻿using System;
using System.Collections.Generic;
using eland.model;
using eland.model.Enums;
using eland.model.States;
using eland.model.Units;
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
            units.Add(new Peasant());
        }

        [Test]
        public void Check_Unit_Movement()
        {
            foreach (var unit in units)
            {
                var context = new MoveStateContext
                                  {
                                      Source = unit,
                                      Target =
                                          new Hex()
                                              {
                                                  HexType = HexType.Grass,
                                                  Id = Guid.NewGuid(),
                                                  Units = null,
                                                  World = null,
                                                  X = 999,
                                                  Y = 888
                                              }
                                  };

                var attackContext = new AttackStateContext();

                if (unit is Soldier)
                {
                    unit.ExecuteTurn(context);
                    Assert.AreEqual(999, unit.Location.X);
                    Assert.AreEqual(888, unit.Location.Y);

                    unit.ExecuteTurn(attackContext);

                }
                if (unit is Peasant)
                {
                    unit.ExecuteTurn(context);
                    Assert.IsNull(unit.Location);
                }

            }
        }

        //[Test]
        //public void Check_Unit_Interfaces()
        //{

        //    foreach (var unit in units)
        //    {
        //        if (unit is Soldier)
        //        {
        //            Assert.IsTrue(unit is IDefensiveUnit);
        //            Assert.IsTrue(unit is IOffensiveUnit);
        //            Assert.IsTrue(unit is IHandToHandUnit);
        //            Assert.IsTrue(unit is IRangedUnit);
        //        }

        //        if (unit is Archer)
        //        {
        //            Assert.IsFalse(unit is IDefensiveUnit);
        //            Assert.IsTrue(unit is IOffensiveUnit);
        //            Assert.IsFalse(unit is IHandToHandUnit);
        //            Assert.IsTrue(unit is IRangedUnit);
        //        }

        //        if (unit is Clubman)
        //        {
        //            Assert.IsTrue(unit is IDefensiveUnit);
        //            Assert.IsTrue(unit is IOffensiveUnit);
        //            Assert.IsTrue(unit is IHandToHandUnit);
        //            Assert.IsFalse(unit is IRangedUnit);
        //        }

        //        if (unit is Peasant)
        //        {
        //            Assert.IsFalse(unit is IOffensiveUnit);
        //            Assert.IsTrue(unit is IDefensiveUnit);
        //            Assert.IsFalse(unit is IHandToHandUnit);
        //            Assert.IsFalse(unit is IRangedUnit);
        //        }

        //    }
        //}

        [Test]
        public void Check_Unit_Properties()
        {
            foreach (var unit in units)
            {
                Assert.IsTrue(unit.Health >= 0);
                Assert.IsTrue(unit.Health <= unit.MaximumHealth);
            }
        }

    }
}
