using System;
using System.Collections.Generic;
using eland.model.Units;

namespace eland.model
{
    public class Nation
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Race Race { get; set; }
        public virtual IList<Unit> Units { get; set; }

        public virtual void AddUnit(Unit unit)
        {
            if(Units == null)
                Units = new List<Unit>();

            unit.Nation = this;
            Units.Add(unit);
        }
    }
}
