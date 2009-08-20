using System;
using System.Collections.Generic;
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
            var spearMan = new Unit {CurrentUnitState = new Spearman()};

            spearMan.ExecuteTurn(new MoveStateContext()
            {
                Source = spearMan,
                Target = new Hex()
                {
                    HexType = HexType.Grass,
                    Id = Guid.NewGuid(),
                    Units = null,
                    World = null,
                    X = 999,
                    Y = 888
                }
            });


        }

     
    }
}
