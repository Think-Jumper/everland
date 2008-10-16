using System;
using System.Collections.Generic;
using MbUnit.Framework;

using eland.model;
using eland.api;
using eland.api.Interfaces;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class WorldTests
    {
        private List<Guid> createdIds;
        private IDataContext dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            createdIds = new List<Guid>();
            dataContext = IoC.Resolve<IDataContext>();
        }

        [TestFixtureTearDown]
        public void Cleanup_Tests()
        {
            using (var tran = dataContext.WorldRepository.Session.BeginTransaction())
            {
                foreach (var id in createdIds)
                {
                    dataContext.WorldRepository.Delete(id);
                }

                tran.Commit();
            }
        }

        [Test]
        [Ignore]
        public void Create_World_And_Hexes()
        {
            var race = new Race { Name = "Default Race" };
            var nation = new Nation { Name = "Default Nation", Race = race };
            var user = new User { Email = "jamie.fraser@gmail.com", FirstName = "Jamie", LastName = "Fraser", OpenId = "http://jamief00.myopenid.com/" };
            var hexType = new HexType { Name = "Default HexType" };
            var game = new Game { Name = "Default Game", Started = DateTime.Now };
            var world = new World { Game = game, Height = 100, Width = 100, Name = "Default World" };
            var gameSession = new GameSession { EnteredGame = DateTime.Now, Nation = nation, Game = game, User = user };

            using (var tran = dataContext.WorldRepository.Session.BeginTransaction())
            {
                dataContext.RaceRepository.Save(race);
                dataContext.NationRepository.Save(nation);
                dataContext.UserRepository.Save(user);
                dataContext.HexTypeRepository.Save(hexType);
                dataContext.GameRepository.Save(game);
                dataContext.WorldRepository.Save(world);
                dataContext.GameSessionRepository.Save(gameSession);

                for (var y = 1; y <= world.Width; y++)
                {
                    for (var x = 1; x <= world.Height; x++)
                    {
                        var hex = new Hex {World = world, HexType = hexType, X = x, Y = y};

                        dataContext.HexRepository.Save(hex);
                    }
                }

                tran.Commit();
            }
        }

        [Test]
        public void World_Create()
        {  
            var world = new World();

            using (var tran = dataContext.WorldRepository.Session.BeginTransaction())
            {
                world.Height = 1000;
                world.Width = 1000;
                world.Name = "unit_test_world";

                dataContext.WorldRepository.Save(world);

                tran.Commit();
            }

            createdIds.Add(world.Id);
            Assert.AreNotEqual(Guid.Empty, world.Id);
        }

        [Test]
        [Ignore]
        public void World_Iterate_Hexes()
        {
            World_Create();
            var world = dataContext.WorldRepository.FindAll();

            foreach (var h in ((World)world[0]).Hexes)
            {
                Assert.AreNotEqual(Guid.Empty, h.Id);
            }
        }

        [Test]
        public void World_Delete()
        {
            var world = new World();

            using (var tran = dataContext.WorldRepository.Session.BeginTransaction())
            {
                world.Height = 1000;
                world.Width = 1000;
                world.Name = "unit_test_world";
            
                dataContext.WorldRepository.Save(world);
                dataContext.WorldRepository.Delete(world);

                tran.Commit();
            }

            Assert.IsNull(dataContext.WorldRepository.Get(world.Id));
        }
    }
}