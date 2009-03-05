using System;
using unitstest.Interfaces;

namespace unitstest
{
    public class PathNode : IComparable<PathNode>
    {
        public IGridShape Position { get; set; }
        public PathNode Parent { get; set; }

        public int G { get; set; }
        public int H { get; set; }
        public int F { get { return G + H; }}

        #region IComparable<PathNode> Members

        public int CompareTo(PathNode other)
        {
            return F != other.F ? F.CompareTo(other.F) : G.CompareTo(other.G);
        }

        #endregion

        # region Overridden Members

        public override bool Equals(object obj)
        {
            return (Position.Id == ((PathNode) obj).Position.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        # endregion

    }
}