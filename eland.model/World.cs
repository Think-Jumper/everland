using System;
using System.Collections.Generic;
using System.Linq;
using eland.model.Enums;

namespace eland.model
{
    public class World
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual IList<Hex> Hexes { get; set; }

        public virtual void AddHex(Hex child)
        {
            if (Hexes == null)
                Hexes = new List<Hex>();
            child.World = this;
            Hexes.Add(child);
        }

        public virtual Hex GetHex(int x, int y)
        {
            return Hexes.Where(hex => hex.X == x && hex.Y == y).Single();
        }

        public int TotalHexes { get { return Hexes.Count; } }
       
        public int TotalHexesOfType(HexType hexType)
        {
            return Hexes.Where(h => h.HexType == hexType).Count();
        }

    }
}
