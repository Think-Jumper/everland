using eland.model.States;

namespace eland.model.UnitCommands
{
    public class NullCommand : IUnitCommand
    {
        public void Execute()
        {
            // intentionally does nothing, funnily enough!
        }
    }
}
