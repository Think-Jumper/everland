
namespace eland.model.Units
{
    public sealed class Archer : Unit
    {
        public Archer()
        {
            MaximumHealth = Consts.UnitConsts.HealthNoncombatPeasantMax;
            Health = Consts.UnitConsts.HealthNoncombatPeasantMax;

            CurrentState = new States.Idle();
        }
      
    }
}