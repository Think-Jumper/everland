using System;
using System.Collections.Generic;
using eland.model.Enums;
using eland.model.Units;

namespace eland.model
{
    public class Hex
    {
        public virtual Guid Id { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual World World { get; set; }
        public virtual HexType HexType { get; set; }
        public virtual IList<Unit> Units { get; set; }

        public virtual bool IsAdjacentTo(Hex hex)
        {
            return true;
        }

        public virtual bool IsTraversable(Unit unit)
        {
            return true;
        }
    }
}
