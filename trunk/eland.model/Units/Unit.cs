using System.Collections.Generic;
using eland.model.Interfaces;

namespace eland.model.Units
{
    public abstract class Unit
    {
        protected Unit()
        {
            Health = 0;
            MaximumHealth = 0;
            Upgrades = new List<IUpgrade>();
        }

        public int Health { get; internal set; }
        public int MaximumHealth { get; internal set; }

        public IList<IUpgrade> Upgrades { get; set; }
    }
}