using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using unitstest.Interfaces;

namespace unitstest
{
    public class AStarPathCalculator : IPathCalculator
    {
        private readonly List<PathNode> _closedList;
        private readonly List<PathNode> _openList;

        public AStarPathCalculator()
        {
            _openList = new List<PathNode>();
            _closedList = new List<PathNode>();
        }

        private static PathNode CalculateFGH(PathNode currentNode, PathNode proposedNode, IGridShape endNode)
        {
            proposedNode.G = currentNode.G + CalculateCost(currentNode, proposedNode);
            proposedNode.H = 10 * (CalculateDistance(endNode.Y2 , proposedNode.Position.Y2 ) +
                                   CalculateDistance(endNode.X2 , proposedNode.Position.X2 ));
            return proposedNode;
        }

        private static int CalculateDistance(int a, int b)
        {
            return Math.Abs(a/Page.grid_size - b/Page.grid_size);
        }

        private static int CalculateCost(PathNode currentNode, PathNode proposedNode)
        {
            return  (IsMoveDiagonal(currentNode, proposedNode)) ? 10 : 14;
        }

        private static bool IsMoveDiagonal(PathNode current, PathNode next)
        {
            var differentRow = current.Position.Row != next.Position.Row;
            var differentColumn = current.Position.Column != next.Position.Column;

            return differentRow && differentColumn ? true : false;
        }

        public List<PathNode> Calculate(IGridShape start, IGridShape end, IGridManager gridManager)
        {
            var startNode = new PathNode {Position = start};
            startNode = CalculateFGH(startNode, startNode, end);
            
            _openList.Add(startNode);

            while (true)
            {
                _openList.Sort();

                if (_openList.Count == 0)
                    //_openList.Add(_closedList[0]);
                    return _closedList;

                var current = _openList[0]; 

                if (current.Position == end)
                    return _closedList;

                _closedList.Add(current);
                _openList.Remove(current);

                var neighbours = gridManager.GetNeighbours(current.Position);

                foreach (var g in neighbours)
                {
                    if (g == null) continue;

                    var c1 = _closedList.Where(x => x.Position == g).FirstOrDefault();

                    if (!g.Blocked && (c1 == null))
                    {
                        var newNode = new PathNode { Position = g, Parent = current };
                        newNode = CalculateFGH(current, newNode, end);

                        if(!_openList.Contains(newNode))
                            _openList.Add(newNode);

                        if (_openList.Contains(newNode))
                        {
                            var existingNode = _openList.First(x => x.Position.Id == newNode.Position.Id);
                            if (newNode.G < existingNode.G)
                            {
                                existingNode.Parent = current;
                                existingNode = CalculateFGH(current, existingNode, end);
                            }
                                


                        }
                    }

                    //if (!g.Blocked && (c1 != null))
                    //{
                    //    var newNode = new PathNode { Position = g, Parent = current };
                    //    newNode = CalculateFGH(current, newNode, end);

                    //    if (newNode.G < c1.G)
                    //    {
                    //        _openList.Remove(c1);
                    //        _openList.Add(newNode);
                    //    }
                    //}
                }


            }

        }
    }
}