using System;
using eland.model.Units;

namespace eland.model.Interfaces
{
    public interface IUpgrade
    {
        string Name { get; }
        string Description { get; }
        void Apply(Unit target);
        bool IsValid(Unit target);
    }

    public class OffensiveUpgrade1 : IUpgrade
    {
        public string Name
        {
            get { return "Base Upgrade 1"; }
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsValid(Unit target)
        {
            return (target is IOffensiveUnit);
        }

        public void Apply(Unit target)
        {
            if (!(target is IOffensiveUnit))
                throw new ArgumentException("The target must be an Offensive Unit");

            target.MaximumHealth = (int)(target.MaximumHealth * 1.25);
            ((IOffensiveUnit) target).AttackStrength += 2;

        }

    }
}