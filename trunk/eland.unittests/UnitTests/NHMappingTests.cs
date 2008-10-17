using eland.api.Helpers;
using MbUnit.Framework;

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
