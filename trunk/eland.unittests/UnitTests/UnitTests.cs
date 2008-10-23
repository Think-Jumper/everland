using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eland.api;
using eland.api.Interfaces;
using eland.model;
using eland.model.Interfaces;
using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private IDataContext dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            dataContext = IoC.Resolve<IDataContext>();

            for(var i = 0; i < 5; i++)
            {
                UnitType unitType;

                ABC abc;


            }

            for(var i = 0; i < 100; i++)
            {
                IUnit unit = new Unit {Name = "test_unit"};
                //unit.Type = 

            }

        }


        [Test]
        public void Check_Unit_Properties()
        {
            var units = dataContext.UnitRepository.GetAll()[0];

            foreach (IUnit unit in settlement.Units)
            {
                Assert.AreNotEqual(unit, null);
                Assert.AreNotEqual(unit.Name, string.Empty);
                Assert.AreNotEqual(unit.Type, null);
                Assert.AreNotEqual(unit.Type.Name, string.Empty);

            }
        }
    }

    internal class Unit : IUnit
    {
        public string Name
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IUnitType Type
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }


}
