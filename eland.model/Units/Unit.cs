using System;
using System.Collections.Generic;

namespace eland.model.Units
{
    public abstract class Unit
    {
        public virtual Hex Location { get; set; }
        public virtual Guid Id { get; set; }
        public virtual int Health { get; set; }
        public virtual int MaximumHealth { get; set; }
        //public virtual IList<IUpgrade> Upgrades { get; set; }
        public virtual Nation Nation { get; set; }
    }
}  