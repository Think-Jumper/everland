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

            if ((currentColumn + 2) % 2 == 0)
            {
                neighbours.Add(_grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow+1) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow+1) && (g.Column == currentColumn )).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn +1)).SingleOrDefault());
            }
            else{
                neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn + 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
                neighbours.Add(_grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn)).SingleOrDefault());
            }

            return neighbours;

        }

        public void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas)
        {
            _elementsX = Math.Floor(surface.ActualWidth / (gridSize * 2));
            _elementsY = Math.Floor(surface.ActualHeight / gridSize);

            double column = 0;
            double row = 0;

            surface.Children.Clear();
            _grid = new List<IGridShape>();
            _gridSize = gridSize;

            var id = 0;
            var rnd = new Random(Environment.TickCount);

            for (var y = 0; y < _elementsY; y++)
            {
                row = (row + 2) % 2 == 0 ? 1 : 0;

                for (var x = 0; x < _elementsX; x++)
                {
                    var xx = ((x + 1) * (gridSize * 2));
                    if ((y+2)%2 != 0 )
                    {
                        xx = ((x + 1) * (gridSize * 2)) - gridSize;
                    }

                    var yy = (int)((y*gridSize) * 0.5);
                    var g = _factory.Create(xx, yy, gridSize, id++, false, (int)Math.Floor(column), (int)row);

                    if (randomiseBlockedAreas)
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            g.Blocked = true;
                            HighLight(surface, g, Colors.Blue);
                        }
                    }
                    
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

                    //var tb = new TextBlock { Text = g.Row + "," + g.Column };
                    //tb.FontSize = 8;
                    //tb.SetValue(Canvas.TopProperty, (double)g.Y1);
                    //tb.SetValue(Canvas.LeftProperty, (double)g.X1);
                    //surface.Children.Add(tb);


                    row += 2;
                }
               
                column += 0.5;
            }

        }

        public void Block(Canvas surface, int x, int y)
        {
            var selectedHex = _grid.Where(hex => hex.Intersects(new Point(x, y))).FirstOrDefault();
            if (selectedHex == null) return;
            selectedHex.Blocked = true;

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
            poly.Fill = new SolidColorBrush(colour);
            poly.StrokeThickness = 1;
            surface.Children.Add(poly);

        }

        public IGridShape HighlightShape(Canvas surface, int x, int y)
        {
            var shape = _grid.Where(hex => hex.Intersects(new Point(x, y))).SingleOrDefault();
            if (shape == null) return null;
           
            HighLight(surface, shape, Colors.Red);
            return shape;
        }

        public void HighlightGridShape(Canvas surface, List<IGridShape> shapes)
        {
            foreach(var shape in shapes)
            {
                HighLight(surface, shape, Colors.Yellow);
            }
        }

        public void HighlightGridShape(Canvas surface, List<PathNode> nodes)
        {
            //foreach (var g in nodes)
            //    HighlightShape(surface, g.Position.X1+1, g.Position.Y1+1);

            var currentNode = nodes[nodes.Count - 1];

            while (true)
            {
                HighlightShape(surface, currentNode.Position.X1, currentNode.Position.Y1);

                if (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                }
                else
                {
                    break;
                }
            }
        }

        public List<PathNode> CalculatePath(IGridShape start, IGridShape end)
        {
            var aStar = new AStarPathCalculator(_gridSize);
            return aStar.Calculate(start, end, this);
        }
    }
}
