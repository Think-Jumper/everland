using System;
using System.Collections.Generic;
using System.Windows;
using unitstest.Interfaces;

namespace unitstest
{
    public class GridHexFactory : IGridShapeFactory<GridHex>
    {
        public GridHex Create(int x, int y, int size, int id, bool blocked, int row, int column, int height)
        {
            return new GridHex(x, y, size, id, blocked, row, column, height);
        }
    }
    
    public class GridHex : IGridShape
    {
        public IList<Point> Points { get; protected set; }
        public int Height { get; set;}

        public GridHex(int x, int y, int size, int id, bool blocked, int row, int column, int height)
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
            Height = height;
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

        public bool Blocked { get; set;}
        public int Row { get; set;}
        public int Column { get; set; }
       
        public bool Intersects(Point point)
        {
            return IsWithinPolygon(6, (int)point.X, (int)point.Y);
        }

        private bool IsWithinPolygon(int nvert, float testx, float testy)
        {
            int i, j;
            var c = false;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((Points[i].Y > testy) != (Points[j].Y > testy)) &&
                 (testx < (Points[j].X - Points[i].X) * (testy - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))
                    c = !c;
            }
            return c;
        }
    }
}
