using System.Collections.Generic;

namespace unitstest.Interfaces
{
    public interface IPathCalculator
    {
        List<PathNode> Calculate(IGridShape start, IGridShape end, IGridManager gridManager);
    }
}