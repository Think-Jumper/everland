

namespace eland.model.Units
{
    public sealed class Clubman : Unit
    {
        public Clubman()
        {
            MaximumHealth = Consts.UnitConsts.HealthNoncombatPeasantMax;
            Health = Consts.UnitConsts.HealthNoncombatPeasantMax;

            AttackState = new States.Attack.HandToHand();
            MoveState = new States.Movement.Foot();
        }
    }
}