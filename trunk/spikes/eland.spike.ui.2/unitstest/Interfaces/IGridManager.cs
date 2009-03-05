using System.Collections.Generic;
using System.Windows.Controls;

namespace unitstest.Interfaces
{
    public interface IGridManager
    {
        List<IGridShape> GetNeighbours(IGridShape centreGridSquare);
        void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas);
        void Block(Canvas surface, int X, int Y);
        IGridShape HighlightGridSquare(Canvas surface, int X, int Y);
        void HighlightGridSquares(Canvas surface, List<IGridShape> squares);
        void HighlightGridSquares(Canvas surface, List<PathNode> squares);
        List<PathNode> CalculatePath(IGridShape start, IGridShape end);
    }
}