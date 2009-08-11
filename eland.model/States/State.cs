using eland.model.Units;

namespace eland.model.States
{
    public abstract class State
    {
        public abstract void Handle(StateContext context);
    }


    public class StateContext
    {
        public Unit Source { get; set; }
        public Hex Target { get; set; }
        //TODO: figure out what needs to go in here so that the state can do what it needs to!
    }

    public class AttackStateContext : StateContext
    {
    }
     
    public class MoveStateContext : StateContext
    {
    }

    public class BuildStateContext : StateContext
    {
    }
}
