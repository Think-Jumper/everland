﻿using eland.model;
using eland.model.Units;
using MbUnit.Framework;

namespace eland.unittests.UnitTests.Units
{
    [TestFixture]
    public class PeasantTests
    {
        private Peasant peasant;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            peasant = new Peasant(new Hex());
        }

        [Test]
        public void Peasant_Has_Location()
        {
            Assert.IsTrue(peasant.Location != null);
        }

        [Test]
        public void Peasant_Can_Move()
        {
            peasant.Move(new Hex());
        }




    }
}