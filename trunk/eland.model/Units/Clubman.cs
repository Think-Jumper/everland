

namespace eland.model.Units
{
    public sealed class Clubman : Unit
    {
        public Clubman()
        {
            MaximumHealth = Consts.UnitConsts.HealthNoncombatPeasantMax;
            Health = Consts.UnitConsts.HealthNoncombatPeasantMax;

            CurrentState = new States.Movement.Movement();
        }
    }
}