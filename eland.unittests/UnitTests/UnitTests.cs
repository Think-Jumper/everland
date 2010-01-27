using eland.api.Services;
using eland.model;
using eland.model.States;
using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void Check_Unit_State_Transitions()
        {
            var world = WorldService.Create(200, 200);

            var spearMan = new Unit
                               {
                                   CurrentUnitState = new Spearman(),
                                   Location = world.GetHex(50, 50)
                               };

            spearMan.ExecuteTurn(new MoveStateContext()
            {
                Source = spearMan,
                Target = world.GetHex(70,70)
            });

            while(spearMan.Location.X != 70 && spearMan.Location.Y != 70)
            {
                var x = spearMan.Location.X;
                var y = spearMan.Location.Y;
                spearMan.ExecuteTurn(new ContinueStateContext());

                Assert.AreNotEqual(x, spearMan.Location.X);
                Assert.AreNotEqual(y, spearMan.Location.Y);
            }

            Assert.AreEqual(70, spearMan.Location.X);
            Assert.AreEqual(70, spearMan.Location.Y);



        }

     
    }
}
