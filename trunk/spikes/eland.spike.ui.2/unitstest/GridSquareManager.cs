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

    public class GridSquareManager<T, TK> : IGridManager where T : IGridShapeFactory<TK> where TK : IGridShape
    {
        private readonly T _factory;
        private double _elementsX;
        private double _elementsY;
        private int _gridSize;
        private List<IGridShape> _grid;

        public GridSquareManager(T factory)
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
            neighbours.Add(_grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn)).SingleOrDefault());
            neighbours.Add(_grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn + 1)).SingleOrDefault());

            return neighbours;
        }

        public void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas)
        {
            _elementsX = Math.Floor(surface.ActualWidth/gridSize);
            _elementsY = Math.Floor(surface.ActualHeight/gridSize);

            surface.Children.Clear();
            _grid = new List<IGridShape>();
            _gridSize = gridSize;

            var id = 0;
            var rnd = new Random(Environment.TickCount);

            for (var y = 0; y < _elementsY; y++)
            {
                for (var x = 0; x < _elementsX; x++)
                {
                    var g = _factory.Create(x, y, gridSize, id++, false, y, x);

                    if (randomiseBlockedAreas)
                        if (rnd.Next(0, 3) == 0)
                        {
                            g.Blocked = true;
                            HighLight(surface, g, Colors.Blue);
                        }
                            

                    _grid.Add(g);
                }
            }

            DrawGridLines(surface, strokeWidth);
        }

        public void Block(Canvas surface, int x, int y)
        {
            foreach (var g in _grid)
            {
                if (!g.Intersects(new Point(x, y))) continue;
                g.Blocked = true;
                HighLight(surface, g, Colors.Blue);
                return;
            }
        }

        public IGridShape HighlightShape(Canvas surface, int x, int y)
        {
            foreach (var g in _grid)
            {
                if (!g.Intersects(new Point(x, y))) continue;
                HighLight(surface, g, Colors.Green);
                return g;
            }

            return null;
        }

        public void HighlightGridShape(Canvas surface, List<IGridShape> shapes)
        {
            foreach (var g in shapes)
            {
                HighLight(surface, g, Colors.Red);
            }
        }

        public void HighlightGridShape(Canvas surface, List<PathNode> nodes)
        {
            foreach(var g in nodes)
                HighLight(surface, g, Colors.Orange);

            var currentNode = nodes[nodes.Count - 1];

            while(true)
            {
                HighLight(surface, currentNode, Colors.Red);

                if (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                }else
                {
                    break;
                }
            }
        }

        private static void HighLight(Panel surface, IGridShape rect, Color colour)
        {
            if (rect == null) return;
            var highlight = new Rectangle {Fill = new SolidColorBrush(colour)};
            highlight.SetValue(Canvas.TopProperty, (double) rect.Y1);
            highlight.SetValue(Canvas.LeftProperty, (double) rect.X1);
            highlight.Width = rect.X2 - rect.X1;
            highlight.Height = rect.Y2 - rect.Y1;

            surface.Children.Add(highlight);
        }

        private static void HighLight(Panel surface, PathNode rect, Color colour)
        {
            if (rect.Position == null) return;

            var highlight = new Rectangle {Fill = new SolidColorBrush(colour)};
            highlight.SetValue(Canvas.TopProperty, (double) rect.Position.Y1);
            highlight.SetValue(Canvas.LeftProperty, (double) rect.Position.X1);
            highlight.Width = rect.Position.X2 - rect.Position.X1;
            highlight.Height = rect.Position.Y2 - rect.Position.Y1;

            surface.Children.Add(highlight);
        }

        private void DrawGridLines(Panel surface, double strokeWidth)
        {
            for (int x = 0; x <= (_elementsX*Page.GridSize); x += Page.GridSize)
            {
                var l = new Line
                            {
                                X1 = x,
                                Y1 = 0,
                                X2 = x,
                                Y2 = Page.GridSize*_elementsY,
                                Stroke = new SolidColorBrush(Colors.DarkGray),
                                StrokeThickness = strokeWidth
                            };
                surface.Children.Add(l);
            }

            for (int y = 0; y <= (_elementsY*Page.GridSize); y += Page.GridSize)
            {
                var l = new Line
                            {
                                X1 = 0,
                                Y1 = y,
                                X2 = Page.GridSize*_elementsX,
                                Y2 = y,
                                Stroke = new SolidColorBrush(Colors.DarkGray),
                                StrokeThickness = strokeWidth
                            };
                surface.Children.Add(l);
            }
        }

        public List<PathNode> CalculatePath(IGridShape start, IGridShape end)
        {
            var aStar = new AStarPathCalculator(_gridSize);
            return aStar.Calculate(start, end, this);
        }
    }
}