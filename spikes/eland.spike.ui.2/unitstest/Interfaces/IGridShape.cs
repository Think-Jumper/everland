using System.Windows;

namespace unitstest.Interfaces
{
    public interface IGridShape
    {
        int Id { get; set; }
        int X1 { get; set; }
        int Y1 { get; set; }
        int X2 { get; set; }
        int Y2 { get; set; }
        int Size { get; set; }
        bool Blocked { get; set; }
        int Row { get; set; }
        int Column { get; set; }
        bool Intersects(Point point);
        IGridShape Create(int x, int y, int size, int id, bool blocked, int row, int column);
    }
}