using System.Collections.Generic;
using System.Windows.Controls;

namespace unitstest.Interfaces
{
    public interface IGridManager
    {
        List<IGridShape> GetNeighbours(IGridShape centreGridSquare);
        void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas);
        void Block(Canvas surface, int x, int y);
        IGridShape HighlightShape(Canvas surface, int x, int y);
        void HighlightGridShape(Canvas surface, List<IGridShape> shapes);
        void HighlightGridShape(Canvas surface, List<PathNode> nodes);
        List<PathNode> CalculatePath(IGridShape start, IGridShape end);
    }
}