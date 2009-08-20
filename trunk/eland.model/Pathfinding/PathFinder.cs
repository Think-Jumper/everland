using System.Collections.Generic;

namespace eland.model.Pathfinding
{
    public static class PathFinder
    {
        //TODO: inject IPathCalculator

        public static IList<Hex> CalculatePath(Hex start, Hex end)
        {
            var currentHex = start;
            var path = new List<Hex>();
            while(!currentHex.IsAdjacentTo(end))
            {
                // need to inject the pathfinder here, which takes into account what a unit can traverse
                // and relative terrain cost, also avoiding enemy units
                currentHex = currentHex.FindClosestSurroundingHexTo(end);
                path.Add(currentHex);
            }

            return path;
        }
    }
}