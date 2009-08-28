using System.Collections.Generic;
using System.Windows;
using unitstest.Interfaces;

namespace unitstest
{
    public class GridSquareFactory : IGridShapeFactory<GridSquare>
    {
        public GridSquare Create(int x, int y, int size, int id, bool blocked, int row, int column)
        {
            return new GridSquare(x, y, size, id, blocked, row, column);
        }
    }

    public class GridSquare : IGridShape
    {
        public GridSquare(int x, int y, int size, int id, bool blocked, int row, int column)
        {
            X1 = (size*x);
            X2 = (size*(x + 1));
            Y1 = (size*y);
            Y2 = (size*(y + 1));
            Id = id;
            Size = size;
            Blocked = blocked;
            Row = row;
            Column = column;
        }

        #region IGridShape Members

        public IList<Point> Points { get; protected set; }
        public int Id { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Size { get; set; }
        public bool Blocked { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public bool Intersects(Point point)
        {
            return ((X1 < point.X && point.X < X2) && (Y1 < point.Y && point.Y < Y2));
        }

        //public IGridShape Create(int x, int y, int size, int id, bool blocked, int row, int column)
        //{
        //    return new GridSquare(x, y, size, id, blocked, row, column);
        //}

        #endregion
    }
}