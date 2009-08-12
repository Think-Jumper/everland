using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using eland.model.Units;

namespace eland.model.States
{
    public abstract class State
    {
        private static readonly Dictionary<Int64, MethodInfo> Dispatch;

        static State()
        {
            Dispatch = new Dictionary<Int64, MethodInfo>();

            foreach (var t in Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(State))))
            {
                foreach (var mi in t.GetMethods().Where(mi => mi.Name == "Handle" && mi.GetParameters().Length > 0))
                {
                    var code = ((Int64)t.GetHashCode() << 32) + mi.GetParameters()[0].ParameterType.GetHashCode();

                    Dispatch.Add(code, mi);
                }
            }
        }

        public virtual void Handle(StateContext context)
        {
            var hash = ((Int64)GetType().GetHashCode() << 32) + context.GetType().GetHashCode();
            if (Dispatch.ContainsKey(hash))
            {
                Dispatch[hash].Invoke(this, new[] { context });
            }
            else
            {
                // the state doesn't explicitly handle this change, so for now use the idle state as a default transition
                context.Source.ChangeState(new Idle());
                context.Source.ExecuteTurn(context);
                Console.WriteLine("Default State being used : " + context);
            }
        }
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

    public class IdleStateContext : StateContext
    {
    }

    public class FortifiedStateContext : StateContext
    {
    }

    public class DefendStateContext : StateContext
    {
    }

    public class BuildStateContext : StateContext
    {
    }
}
