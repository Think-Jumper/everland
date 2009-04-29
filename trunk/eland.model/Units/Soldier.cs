using eland.model.Consts;
using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Soldier : Unit, IRangedUnit, IHandToHandUnit
    {
        public Soldier()
        {
            MaximumHealth = UnitConsts.ATTACK_OFFENSIVE_SOLDIER_MAX;
            Health = MaximumHealth;
            AttackStrength = UnitConsts.ATTACK_OFFENSIVE_SOLDIER_MAX;
            Range = UnitConsts.RANGE_OFFENSIVE_SOLDIER_MAX;
        }

        public int Range { get; set; }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public int AttackStrength { get; set; }

        public void Defend()
        {
            throw new System.NotImplementedException();
        }
    }
}