using System;

namespace eland.model.States.Attack
{
    public class Ranged : State
    {
        public override void Handle(StateContext context)
        {
            //rubbish, but you get the idea
            if (context.Target.Units.Count == 0)
                context.Source.ExecuteTurn(context as MoveStateContext);
            else if (context.Target.IsAdjacentTo(context.Source.Location))
            {
                context.Source.Health -= 1;
            }
            else
            {
                var target = context.Target.Units[0];
                target.Health -= 1;
            }

            // some kind of property change notification thing
        }
    }
}
