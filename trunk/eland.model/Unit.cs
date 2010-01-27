using System;
using eland.model.Enums;
using eland.model.States;

namespace eland.model
{
    public class Unit
    {
        public virtual Hex Location { get; set; }
        public virtual Guid Id { get; set; }
        public virtual int Health { get; set; }
        public virtual int MaximumHealth { get; set; }
        public virtual Nation Nation { get; set; }

        public UnitState CurrentUnitState { get; set; }

        public void ExecuteTurn(TurnContext context)
        {
            CurrentUnitState.Handle(context).Execute();
        }

        public bool CanTraverse(Hex hex)
        {
            return CurrentUnitState.CanTraverse(hex);
        }

    }

}  