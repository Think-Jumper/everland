using System;
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

        public virtual UnitState CurrentUnitState { get; set; }

        public virtual void ExecuteTurn(TurnContext context)
        {
            CurrentUnitState.Handle(context).Execute();
        }

        public virtual bool CanTraverse(Hex hex)
        {
            return CurrentUnitState.CanTraverse(hex);
        }

    }

}  