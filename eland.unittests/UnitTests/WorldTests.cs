using System;
using eland.api;
using eland.model.Enums;
using MbUnit.Framework;

using eland.model;
using eland.api.Interfaces;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class WorldTests
    {
        private Guid _worldId;
        private IDataContext _dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            _dataContext = IoC.Resolve<IDataContext>();

            var world = new World();

            using (var tran = _dataContext.WorldRepository.Session.BeginTransaction())
            {
                world.Height = 5;
                world.Width = 5;
                world.Name = "unit_test_world";

                for (var y = 1; y <= world.Width; y++)
                {
                    for (var x = 1; x <= world.Height; x++)
                    {
                        world.AddHex(new Hex { World = world, HexType = HexType.Plain , X = x, Y = y });
                    }
                }

                _dataContext.WorldRepository.Save(world);
                tran.Commit();
            }

            _worldId = world.Id;
        }

        [TestFixtureTearDown]
        public void Cleanup_Tests()
        {
            using (var tran = _dataContext.WorldRepository.Session.BeginTransaction())
            {
                _dataContext.WorldRepository.Delete(_worldId);

                tran.Commit();
            }
        }

        [Test]
        public void World_Iterate_Hexes()
        {
            var world = GetWorld();

            foreach (var h in (world).Hexes)
                Assert.AreNotEqual(Guid.Empty, h.Id);
        }

        [Test]
        public void World_Iterate_Properties()
        {
            var world = GetWorld();

            Assert.IsTrue(world.Height > 0);
            Assert.IsTrue(world.Width > 0);
        }

        [Test]
        public void World_Iterate_Hex_Properties()
        {
            var world = GetWorld();

            foreach (var h in (world).Hexes)
            {
                Assert.AreNotEqual(null, h.HexType);
                Assert.Between(h.X, 0, world.Width);
                Assert.Between(h.Y, 0, world.Height);
            }
        }

        private World GetWorld()
        {
            return _dataContext.WorldRepository.Get(_worldId);
        }

        # region Ignored Tests

        [Test]
        [Ignore]
        public void Create_World_And_Hexes()
        {
            var race = new Race { Name = "Default Race" };
            var nation = new Nation { Name = "Default Nation", Race = race };
            var user = new User { Email = "jamie.fraser@gmail.com", FirstName = "Jamie", LastName = "Fraser", OpenId = "http://jamief00.myopenid.com/" };
            var world = new World { Height = 100, Width = 100, Name = "Default World" };
            var game = new Game { Name = "Default Game", Started = DateTime.Now, GameWorld = world};
            var gameSession = new GameSession { EnteredGame = DateTime.Now, Nation = nation, Game = game, User = user };

            using (var tran = _dataContext.WorldRepository.Session.BeginTransaction())
            {
                _dataContext.RaceRepository.Save(race);
                _dataContext.NationRepository.Save(nation);
                _dataContext.UserRepository.Save(user);
                _dataContext.GameRepository.Save(game);
                _dataContext.WorldRepository.Save(world);
                _dataContext.GameSessionRepository.Save(gameSession);

                for (var y = 1; y <= world.Width; y++)
                {
                    for (var x = 1; x <= world.Height; x++)
                    {
                        var hex = new Hex { World = world, HexType = HexType.Grass, X = x, Y = y };

                        _dataContext.HexRepository.Save(hex);
                    }
                }

                tran.Commit();
            }
        }

        [Test]
        [Ignore]
        public void World_Delete()
        {
            var world = new World();

            using (var tran = _dataContext.WorldRepository.Session.BeginTransaction())
            {
                world.Height = 1000;
                world.Width = 1000;
                world.Name = "unit_test_world";

                _dataContext.WorldRepository.Save(world);
                _dataContext.WorldRepository.Delete(world);

                tran.Commit();
            }

            Assert.IsNull(_dataContext.WorldRepository.Get(world.Id));
        }




        #endregion

    }
}