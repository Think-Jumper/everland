using System;
using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Archer : Unit, IRangedUnit
    {
        public int Range
        {
            get { return 0; }
            set { throw new NotImplementedException(); }
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public int AttackStrength
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}