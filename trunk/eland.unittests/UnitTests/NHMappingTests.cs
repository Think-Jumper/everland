using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;
using eland.api;

namespace eland.unittests.UnitTests
{
   [TestFixture]
   public class NHMappingTests
   {
      [Test]
      public void Mappings_Are_Valid()
      {
         NHibHelper.GetCurrentSession();
      }

   }
}
