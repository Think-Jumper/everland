using eland.model.States;

namespace eland.model.UnitCommands
{
    public class AttackCommand : IUnitCommand
    {
        private readonly AttackStateContext _attackContext;

        public AttackCommand(AttackStateContext attackContext)
        {
            _attackContext = attackContext;
        }

        #region IUnitCommand Members

        public void Execute()
        {
            // inject combat engine or similar
            _attackContext.Target.GetDefendingUnit().Health -= 1;
        }

        #endregion

    }
}
