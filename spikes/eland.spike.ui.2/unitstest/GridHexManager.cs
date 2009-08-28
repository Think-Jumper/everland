using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using unitstest.Interfaces;

namespace unitstest
{
    public class GridHexManager<T, TK> : IGridManager where T : IGridShapeFactory<TK> where TK : IGridShape
    {
        private readonly T _factory;
        private double _elementsX;
        private double _elementsY;
        private int _gridSize;
        private List<IGridShape> _grid;

        public GridHexManager(T factory)
        {
            _factory = factory;
        }

        public List<IGridShape> GetNeighbours(IGridShape centreGridSquare)
        {
            var neighbours = new List<IGridShape>();
            var currentColumn = centreGridSquare.Column;
            var currentRow = centreGridSquare.Row;

            neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn + 1)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn)).SingleOrDefault());

            return neighbours;

        }

        public void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas)
        {
            _elementsX = 20;
            _elementsY = 50;

            surface.Children.Clear();
            _grid = new List<IGridShape>();
            _gridSize = gridSize;

            var id = 0;

            for (var y = 0; y < _elementsY; y++)
            {
                for (var x = 0; x < _elementsX; x++)
                {
                    var xx = ((x + 1) * (gridSize * 2));
                    if ((y+2)%2 != 0 )
                    {
                        xx = ((x + 1) * (gridSize * 2)) - gridSize;
                    }
                  

                    var yy = (int)((y*gridSize) * 0.5);// +(gridSize);
                    var g = _factory.Create(xx, yy, gridSize, id++, false, y, x);
                    
                    var poly = new Polygon();
                    var points = new PointCollection();

                    foreach (var p in g.Points) {
                        points.Add(p);
                    }

                    poly.Points = points;
                    poly.Stroke = new SolidColorBrush(Colors.DarkGray);
                    poly.StrokeThickness = 1;
                    surface.Children.Add(poly);

                    _grid.Add(g);
                }
            }

        }

        public void Block(Canvas surface, int x, int y)
        {
            var selectedHex = _grid.Where(hex => hex.Intersects(new Point(x, y))).FirstOrDefault();

            HighLight(surface, selectedHex, Colors.Blue);

        }

        private static void HighLight(Panel surface, IGridShape hex, Color colour)
        {
            if (hex == null) return;
            var poly = new Polygon();
            var points = new PointCollection();

            foreach (var p in hex.Points)
            {
                points.Add(p);
            }

            poly.Points = points;
            poly.Stroke = new SolidColorBrush(Colors.LightGray);
            poly.Fill = new SolidColorBrush(Colors.Red);
            poly.StrokeThickness = 1;
            surface.Children.Add(poly);

        }

        public IGridShape HighlightGridSquare(Canvas surface, int X, int Y)
        {
            throw new NotImplementedException();
        }

        public void HighlightGridSquares(Canvas surface, List<IGridShape> squares)
        {
            throw new NotImplementedException();
        }

        public void HighlightGridSquares(Canvas surface, List<PathNode> squares)
        {
            throw new NotImplementedException();
        }

        public List<PathNode> CalculatePath(IGridShape start, IGridShape end)
        {
            var aStar = new AStarPathCalculator(_gridSize);
            return aStar.Calculate(start, end, this);
        }
    }
}
