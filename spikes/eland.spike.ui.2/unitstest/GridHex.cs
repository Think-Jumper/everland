using System;
using System.Collections.Generic;
using System.Windows;
using unitstest.Interfaces;

namespace unitstest
{
    public class GridHexFactory : IGridShapeFactory<GridHex>
    {
        public GridHex Create(int x, int y, int size, int id, bool blocked, int row, int column)
        {
            return new GridHex(x, y, size, id, blocked, row, column);
        }
    }


    public class GridHex : IGridShape
    {
        public IList<Point> Points { get; protected set; }

        public GridHex(int x, int y, int size, int id, bool blocked, int row, int column)
        {
            Points = new List<Point>
                         {
                             new Point(x, y),
                             new Point(x+(size/2), y),
                             new Point(x+(size), y+ (size/2)),
                             new Point(x+(size/2), y+size),
                             new Point(x, y+size),
                             new Point(x-(size/2), y+(size/2))
                         };

            Id = id;
            Blocked = blocked;
            Row = row;
            Column = column;
        }

        public int Id { get; set; }

        public int X1
        {
            get { return (int)Points[0].X; }
            set { }
        }

        public int Y1
        {
            get { return (int)Points[0].Y; }
            set { }
        }

        public int X2
        {
            get { return (int)Points[3].X; }
            set { }
        }

        public int Y2
        {
            get { return (int)Points[3].Y; }
            set { }
        }

        public int Size { get; set; }
        public bool Blocked { get; set;}
        public int Row { get; set;}
        public int Column { get; set; }
       
        public bool Intersects(Point point)
        {
            return ((X1 < point.X && point.X < X2) && (Y1 < point.Y && point.Y < Y2));
        }

        //public IGridShape Create(int x, int y, int size, int id, bool blocked, int row, int column)
        //{
        //    return new GridHex(x, y, size)
        //}
    }
}
