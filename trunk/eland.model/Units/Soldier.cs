using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Soldier : Unit, IRangedUnit, IHandToHandUnit
    {
        public int Range
        {
            get { return 0; }
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Defend()
        {
            throw new System.NotImplementedException();
        }
    }
}