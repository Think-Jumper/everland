using System;
using eland.model.Enums;
using eland.model.Pathfinding;
using eland.model.UnitCommands;
namespace eland.model.States
{
    public class Spearman : UnitState
    {
        public IUnitCommand Handle(AttackStateContext context)
        {
            if (context.Source.Location.IsAdjacentTo(context.Target))
                return new AttackCommand(context);

            return Handle(new MoveStateContext { Source = context.Source, Target = context.Target});
        }

        public IUnitCommand Handle(MoveStateContext context)
        {
            return new MovementCommand(new MoveStateContext { Source = context.Source, Target = context.Target });
        }

        // this should probably be IUnitCommand Handle(ProposedMoveContext) which would return null command if 
        // the location wasn't traversable.
        public override bool CanTraverse(Hex hex)
        {
            return hex.HexType != HexType.Mountain;
        }
    }
}
