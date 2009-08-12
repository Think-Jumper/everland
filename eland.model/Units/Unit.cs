using System;
using eland.model.States;

namespace eland.model.Units
{
    public abstract class Unit
    {
        public virtual Hex Location { get; set; }
        public virtual Guid Id { get; set; }
        public virtual int Health { get; set; }
        public virtual int MaximumHealth { get; set; }
        public virtual Nation Nation { get; set; }

        public State CurrentState { get; set; }

        public void ExecuteTurn(StateContext context)
        {
            CurrentState.Handle(context);
        }

        //public abstract void ChangeState(StateContext context);
    }


}  