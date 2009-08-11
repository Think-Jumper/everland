using System;

namespace eland.model.States.Movement
{
    public class Foot : State
    {
        public override void Handle(StateContext context)
        {
            // totally naive, just move to target instantly! - need to plug in pathfinder etc

            // call pathfinder, get linked list of hexes to traverse

            context.Source.Location = context.Target;
            
        }
    }
}
