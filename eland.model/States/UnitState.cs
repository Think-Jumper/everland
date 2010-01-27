using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using eland.model.UnitCommands;

namespace eland.model.States
{
    public abstract class UnitState
    {
        private static readonly Dictionary<Int64, MethodInfo> Dispatch;
        protected TurnContext CurrentContext { get; set; }

        static UnitState()
        {
            Dispatch = new Dictionary<Int64, MethodInfo>();

            foreach (var t in Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(UnitState))))
            {
                foreach (var mi in t.GetMethods().Where(mi => mi.Name == "Handle" && mi.GetParameters().Length > 0))
                {
                    var code = ((Int64)t.GetHashCode() << 32) + mi.GetParameters()[0].ParameterType.GetHashCode();

                    Dispatch.Add(code, mi);
                }
            }
        }

        public virtual IUnitCommand Handle(TurnContext context)
        {
            if (context.GetType() != typeof(ContinueStateContext))
            {
                CurrentContext = context;
            } else
            {
                context = CurrentContext;
            }
            var hash = ((Int64)GetType().GetHashCode() << 32) + context.GetType().GetHashCode();
            if (Dispatch.ContainsKey(hash))
            {
                return Dispatch[hash].Invoke(this, new[] { context }) as IUnitCommand;
            }

            return new NullCommand();
        }

        public abstract bool CanTraverse(Hex hex);

    }


    public class TurnContext
    {
        public Unit Source { get; set; }
        public Hex Target { get; set; }
        //figure out what needs to go in here so that the state can do what it needs to!
    }

    public interface IUnitCommand
    {
        void Execute();
    }

    public class AttackStateContext : TurnContext {}

    public class ContinueStateContext : TurnContext {}

    public class MoveStateContext : TurnContext
    {
        public Queue<Hex> Path { get; set; }
    }

    public class BuildStateContext : TurnContext {}
   
}
