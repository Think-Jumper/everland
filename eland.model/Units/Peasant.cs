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

        public virtual void Defend()
        {
            throw new System.NotImplementedException();
        }
    }
}