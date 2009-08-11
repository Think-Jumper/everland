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

        public State AttackState { get; set; }
        public State MoveState { get; set; }
        public State BuildState { get; set; }

        public void ExecuteTurn(StateContext context)
        {
            if (context is AttackStateContext)
                AttackState.Handle(context);
            if (context is MoveStateContext)
                MoveState.Handle(context);
            if (context is BuildStateContext)
                BuildState.Handle(context);

            // defend state
            // etc

        }
    }


}  