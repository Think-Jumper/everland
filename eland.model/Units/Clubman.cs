using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Clubman : Unit, IHandToHandUnit
    {
        public virtual void Attack()
        {
            throw new System.NotImplementedException();
        }

        public virtual int AttackStrength
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public virtual void Defend()
        {
            throw new System.NotImplementedException();
        }
    }
}