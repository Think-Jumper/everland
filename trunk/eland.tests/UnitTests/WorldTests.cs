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

      [Test]
      public void World_Create() 
      {
         World world = new World();
         world.Height = 1000;
         world.Width = 1000;
         world.Name = "unit_test_world";

         Repository<World> worldRep = new Repository<World>();
         world = worldRep.Save(world);

         Assert.AreNotEqual(Guid.Empty, world.Id);
      }

      public void World_Delete()
      {

      }
   }
}
