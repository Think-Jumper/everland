using System;
using System.Collections.Generic;
using eland.api.Services;
using eland.model;
using eland.model.Enums;
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


        }

     
    }
}
