namespace eland.model.States
{
    public class Movement : State
    {
        public void Handle(MoveStateContext context)
        {
            // totally naive, just move to target instantly! - need to plug in pathfinder etc

            // call pathfinder, get linked list of hexes to traverse

            context.Source.Location = context.Target;
            
        }
    }
}
