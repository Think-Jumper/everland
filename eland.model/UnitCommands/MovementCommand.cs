using eland.model.Pathfinding;
using eland.model.States;

namespace eland.model.UnitCommands
{
    public class MovementCommand : IUnitCommand
    {
        private readonly MoveStateContext _movementContext;

        public MovementCommand(MoveStateContext movementContext)
        {
            _movementContext = movementContext;
            _movementContext.Path = PathFinder.CalculatePath(movementContext.Source.Location, movementContext.Target);
        }

        #region IUnitCommand Members

        public void Execute()
        {
            _movementContext.Source.Location = _movementContext.Target;
        }

        #endregion

    }
}
