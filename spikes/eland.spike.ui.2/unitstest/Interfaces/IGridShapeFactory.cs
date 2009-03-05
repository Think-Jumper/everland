
namespace unitstest.Interfaces
{
    public interface IGridShapeFactory<T>
    {
        T Create(int x, int y, int size, int id, bool blocked, int row, int column);
    }
}
