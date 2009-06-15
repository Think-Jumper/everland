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

        public virtual int Range { get; set; }

        public virtual void Attack()
        {
            throw new System.NotImplementedException();
        }

        public virtual int AttackStrength { get; set; }

        public virtual void Defend()
        {
            throw new System.NotImplementedException();
        }
    }
}