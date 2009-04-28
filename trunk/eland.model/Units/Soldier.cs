using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Soldier : Unit, IRangedUnit, IHandToHandUnit
    {
        public Soldier()
        {
            MaximumHealth = 100;
            Health = 100;
            AttackStrength = 10;
            Range = 0;
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