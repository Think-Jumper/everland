using eland.model.Interfaces;

namespace eland.model.Units
{
    public class Peasant : Unit, IDefensiveUnit
    {

        public Peasant()
        {
            MaximumHealth = Consts.UnitConsts.HEALTH_NONCOMBAT_PEASANT_MAX;
            Health = MaximumHealth;
        }

        public void Defend()
        {
            throw new System.NotImplementedException();
        }

        public void Move(Hex hex)
        {
            throw new System.NotImplementedException();
        }
    }
}