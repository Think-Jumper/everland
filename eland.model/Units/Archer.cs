
namespace eland.model.Units
{
    public sealed class Archer : Unit
    {
        public Archer()
        {
            MaximumHealth = Consts.UnitConsts.HealthNoncombatPeasantMax;
            Health = Consts.UnitConsts.HealthNoncombatPeasantMax;

            AttackState = new States.Attack.Ranged();
            MoveState = new States.Movement.Foot();
        }
      
    }
}