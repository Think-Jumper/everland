namespace eland.model.States
{
    public class Idle : State
    {
        public void Handle(MoveStateContext context)
        {
            context.Source.ChangeState(new Movement());
            context.Source.ExecuteTurn(context);
        }

        public void Handle(AttackStateContext context)
        {
            context.Source.ChangeState(new Attack());
            context.Source.ExecuteTurn(context);
        }

        public void Handle(DefendStateContext context)
        {
            context.Source.ChangeState(new Defend());
            context.Source.ExecuteTurn(context);
        }

        public void Handle(FortifiedStateContext context)
        {
            context.Source.ChangeState(new Fortified());
            context.Source.ExecuteTurn(context);
        }

        public void Handle(IdleStateContext context)
        {
            // increment unit age or something
            // context.Source.Age++;
        }
    }
}
