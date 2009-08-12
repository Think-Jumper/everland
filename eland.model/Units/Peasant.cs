namespace eland.model.Units
{
    public sealed class Peasant : Unit
    {
        public Peasant()
        {
            MaximumHealth = Consts.UnitConsts.HealthNoncombatPeasantMax;
            Health = Consts.UnitConsts.HealthNoncombatPeasantMax;

            CurrentState = new States.Movement();
        }
    }
}