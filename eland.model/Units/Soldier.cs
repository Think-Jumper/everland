using eland.model.Consts;

namespace eland.model.Units
{
    public sealed class Soldier : Unit
    {
        public Soldier()
        {
            MaximumHealth = UnitConsts.AttackOffensiveSoldierMax;
            Health = UnitConsts.AttackOffensiveSoldierMax;

            CurrentState = new States.Idle();
        }
    }
}