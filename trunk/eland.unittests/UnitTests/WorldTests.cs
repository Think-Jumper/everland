using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using eland.model;
using eland.api;

using MbUnit.Framework;
using System.Collections;
using Query;
using eland.api.Interfaces;
using NHibernate;

namespace eland.tests.UnitTests
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
         using (ITransaction tran = dataContext.WorldRepository.Session.BeginTransaction())
         {
            foreach (Guid id in createdIds)
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
         Race race = new Race() { Name = "Default Race" };
         Nation nation = new Nation { Name = "Default Nation", Race = race };
         User user = new User { Email = "jamie.fraser@gmail.com", FirstName = "Jamie", LastName = "Fraser", OpenId = "http://jamief00.myopenid.com/" };
         HexType hexType = new HexType { Name = "Default HexType" };
         Game game = new Game() { Name = "Default Game", Started = DateTime.Now };
         World world = new World() { Game = game, Height = 100, Width = 100, Name = "Default World" };
         GameSession gameSession = new GameSession() { EnteredGame = DateTime.Now, Nation = nation, Game = game, User = user };

         using (ITransaction tran = dataContext.WorldRepository.Session.BeginTransaction())
         {
            dataContext.RaceRepository.Save(race);
            dataContext.NationRepository.Save(nation);
            dataContext.UserRepository.Save(user);
            dataContext.HexTypeRepository.Save(hexType);
            dataContext.GameRepository.Save(game);
            dataContext.WorldRepository.Save(world);
            dataContext.GameSessionRepository.Save(gameSession);

            for (int y = 1; y <= world.Width; y++)
            {
               for (int x = 1; x <= world.Height; x++)
               {
                  Hex hex = new Hex();
                  hex.World = world;
                  hex.HexType = hexType;
                  hex.X = x;
                  hex.Y = y;

                  dataContext.HexRepository.Save(hex);
               }
            }

            tran.Commit();
         }
      }

      [Test]
      public void World_Create()
      {  
         World world = new World();

         using (ITransaction tran = dataContext.WorldRepository.Session.BeginTransaction())
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
         this.World_Create();
         IList world = dataContext.WorldRepository.FindAll();

         foreach (Hex h in ((World)world[0]).Hexes)
         {
            Assert.AreNotEqual(Guid.Empty, h.Id);
         }
      }

      [Test]
      public void World_Delete()
      {
         World world = new World();

         using (ITransaction tran = dataContext.WorldRepository.Session.BeginTransaction())
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