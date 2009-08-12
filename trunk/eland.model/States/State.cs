using System;
using System.Collections.Generic;
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

            foreach (var t in Assembly.GetCallingAssembly().GetTypes())
            {
                if (!t.IsSubclassOf(typeof(State))) continue;
                foreach (var mi in t.GetMethods())
                {
                    if (mi.Name != "Handle") continue;
                    var pars = mi.GetParameters();
                    if (pars.Length != 1) continue;
                    var code = ((Int64)t.GetHashCode() << 32) + pars[0].ParameterType.GetHashCode();
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
                Console.WriteLine("Handling object " + context);
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
