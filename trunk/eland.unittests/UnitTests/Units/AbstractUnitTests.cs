using System.Collections.Generic;
using eland.api;
using eland.api.Interfaces;
using eland.model.Units;
using MbUnit.Framework;

namespace eland.unittests.UnitTests.Units
{
    [TestFixture]
    public class AbstractUnitTests
    {
        private IDataContext _dataContext;
        private List<Unit> _units;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            _dataContext = IoC.Resolve<IDataContext>();
            _units = new List<Unit> {new Peasant(), new Soldier(), new Archer(), new Clubman()};
        }

        [TestFixtureTearDown]
        public void Cleanup_Tests()
        {
            using( var tran = _dataContext.UnitRepository.Session.BeginTransaction() )
            {
                foreach( var unit in _units )
                    _dataContext.UnitRepository.Delete( unit );

                tran.Commit();
            }

        }

        [Test]
        public void Create_Units()
        {

            using( var tran = _dataContext.UnitRepository.Session.BeginTransaction() )
            {
                foreach(var unit in _units)
                    _dataContext.UnitRepository.Save( unit );

                tran.Commit();
           }
        }

        [Test]
        public void Get_Units()
        {
            var units = _dataContext.UnitRepository.FindAll();

            Assert.IsTrue( units.Count > 0 );

            foreach( Unit unit in units )
            {
                Assert.IsTrue( unit != null );
                Assert.IsTrue( _units.Contains(unit));
                Assert.IsTrue( ( unit is Archer ) || ( unit is Clubman ) || ( unit is Peasant ) || ( unit is Soldier ) );
            }
        }

    }
}
