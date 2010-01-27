using eland.model.States;

namespace eland.model.UnitCommands
{
    public class MovementCommand : IUnitCommand
    {
        private readonly MoveStateContext _movementContext;

        public MovementCommand(MoveStateContext movementContext)
        {
            _movementContext = movementContext;
        }

        #region IUnitCommand Members

        public void Execute()
        {
            var range = 1; // _movementContext.Source.Range

            for(var movementStep = 0; movementStep<range; movementStep++)
            {
                _movementContext.Source.Location = _movementContext.Path.Dequeue();
                // need to actually do a .PerformMove here, checking for existence of enemy units, blocked terrain etc.
            }


        }

        #endregion

    }
}
