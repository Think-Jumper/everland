using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Archer : Unit, IRangedUnit
    {
        public int Range
        {
            get { return 0; }
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}