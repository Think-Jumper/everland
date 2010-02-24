using System;
using System.Collections.Generic;
using eland.model.Enums;
using System.Linq;

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

        public virtual IEnumerable<Hex> SurroundingHexes
        {
            get
            {
                var coords = new[]
				             	{
				             		new[] {0, -1}, //above
				             		new[] {1, -1}, //above and right
				             		new[] {1, 0}, //right
				             		new[] {1, 1}, //below and right
				             		new[] {0, 1}, //below
				             		new[] {-1, 1}, //below and left
				             		new[] {-1, 0}, //left
				             		new[] {-1, -1} //above and left
				             	};
                foreach (var coord in coords)
                {
                    var hex = World.GetHex(X + coord[0], Y + coord[1]);
                    if (hex != null)
                        yield return hex;
                }
            }
        }

        public virtual Hex FindClosestSurroundingHexTo(Hex targetHex)
        {
            var currentDistance = DistanceTo(targetHex);
            return currentDistance == 0
                    ? this 
                    : currentDistance == 1
                        ? targetHex 
                        : SurroundingHexes.OrderBy(hex => hex.DistanceTo(targetHex)).FirstOrDefault();
        }

        public virtual Hex FindFurthestSurroundingHexTo(Hex targetHex)
        {
            return SurroundingHexes.OrderByDescending(hex => hex.DistanceTo(targetHex)).FirstOrDefault();
        }

        public virtual int DistanceTo(Hex hex)
        {
            return Math.Abs(X - hex.X) + Math.Abs(Y - hex.Y);
        }

        public virtual bool IsAdjacentTo(Hex hex)
        {
            return DistanceTo(hex) == 1;
        }

        public virtual bool IsTraversable(Unit unit)
        {
            return true;
        }

        public virtual Unit GetDefendingUnit()
        {
            return Units.OrderByDescending(unit => unit.Health).First();
        }
    }
}
