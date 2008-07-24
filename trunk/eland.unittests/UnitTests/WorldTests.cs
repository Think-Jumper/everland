using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using eland.model;
using eland.api;

using MbUnit.Framework;

namespace eland.tests.UnitTests
{
   [TestFixture]
   public class WorldTests
   {
      private List<Guid> _createdIds;
      private Repository<World> _worldRep;

      [TestFixtureSetUp]
      public void Setup_Tests()
      {
         _createdIds = new List<Guid>();
         _worldRep = new Repository<World>();
      }

      [TestFixtureTearDown]
      public void Cleanup_Tests()
      {
         foreach (Guid id in _createdIds)
         {
            _worldRep.Delete(_worldRep.Get(id));
         }
      }

      [Test]
      [Ignore]
      public void Create_World_And_Hexes()
      {
         World world = new World();
         world.Name = "default";
         world.Height = 10;
         world.Width = 30;

         _worldRep.Save(world);

         HexType hexType = new HexType();
         using (Repository<HexType> hexTypeRep = new Repository<HexType>())
         {
            hexType.Name = "Grassland";
            hexTypeRep.Save(hexType);
         }

         using (Repository<Hex> hexRep = new Repository<Hex>())
         {
            for (int y = 1; y <= 10; y++)
            {
               for (int x = 1; x <= 30; x++)
               {
                  Hex hex = new Hex();
                  hex.World = world;
                  hex.HexType = hexType;
                  hex.X = x;
                  hex.Y = y;

                  hexRep.Save(hex);
               }
            }
         }



      }

      [Test]
      public void World_Create()
      {
         World world = new World();
         world.Height = 1000;
         world.Width = 1000;
         world.Name = "unit_test_world";

         _worldRep.Save(world);
         _createdIds.Add(world.Id);

         Assert.AreNotEqual(Guid.Empty, world.Id);
      }

      [Test]
      public void World_Iterate_Hexes()
      {
         World world = _worldRep.Get(new Guid("2AFE80D8-089D-4075-8231-D56199B49EFE"));

         foreach (Hex h in world.Hexes)
         {
            Assert.AreNotEqual(Guid.Empty, h.Id);
         }
      }

      [Test]
      public void World_Delete()
      {
         World world = new World();
         world.Height = 1000;
         world.Width = 1000;
         world.Name = "unit_test_world";

         world = _worldRep.Save(world);

         _worldRep.Delete(world);

         Assert.IsNull(_worldRep.Get(world.Id));
      }
   }
}
