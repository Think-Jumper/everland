namespace eland.model.States
{
    public class Attack : State
    {
        public void Handle(AttackStateContext context)
        {
            //rubbish, but you get the idea
            if (context.Target.Units.Count == 0)
                context.Source.ExecuteTurn(new MoveStateContext {Source = context.Source, Target = context.Target});
            else if (context.Target.IsAdjacentTo(context.Source.Location))
            {
                context.Source.Health -= 1;
            }
            else
            {
                var target = context.Target.Units[0];
                target.Health -= 1;
            }

            // some kind of property change notification thing
        }
    }
}
