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

    public class GridManager<T, K> : IGridManager where T : IGridShapeFactory<K> where K : IGridShape
    {
        private readonly T factory;
        private double elementsX;
        private double elementsY;
        private List<IGridShape> grid;

        public GridManager(T factory)
        {
            this.factory = factory;
        }

        #region IGridManager Members

        public List<IGridShape> GetNeighbours(IGridShape centreGridSquare)
        {
            var neighbours = new List<IGridShape>();
            int currentColumn = centreGridSquare.Column;
            int currentRow = centreGridSquare.Row;

            neighbours.Add(grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn + 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn + 1)).SingleOrDefault());

            return neighbours;
        }

        #endregion

        public void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas)
        {
            elementsX = Math.Floor(surface.ActualWidth/gridSize);
            elementsY = Math.Floor(surface.ActualHeight/gridSize);

            surface.Children.Clear();
            grid = new List<IGridShape>();

            int id = 0;
            var rnd = new Random(Environment.TickCount);

            for (int y = 0; y < elementsY; y++)
            {
                for (int x = 0; x < elementsX; x++)
                {
                    K g = factory.Create(x, y, gridSize, id++, false, y, x);

                    if (randomiseBlockedAreas)
                        if (rnd.Next(0, 3) == 0)
                        {
                            g.Blocked = true;
                            HighLight(surface, g, Colors.Blue);
                        }
                            

                    grid.Add(g);
                }
            }

            DrawGridLines(surface, strokeWidth);
        }

        public void Block(Canvas surface, int X, int Y)
        {
            foreach (IGridShape g in grid)
            {
                if (!g.Intersects(new Point(X, Y))) continue;
                g.Blocked = true;
                HighLight(surface, g, Colors.Blue);
                return;
            }
        }

        public IGridShape HighlightGridSquare(Canvas surface, int X, int Y)
        {
            foreach (IGridShape g in grid)
            {
                if (!g.Intersects(new Point(X, Y))) continue;
                HighLight(surface, g, Colors.Green);
                return g;
            }

            return null;
        }

        public void HighlightGridSquares(Canvas surface, List<IGridShape> squares)
        {
            foreach (IGridShape g in squares)
            {
                HighLight(surface, g, Colors.Red);
            }
        }

        public void HighlightGridSquares(Canvas surface, List<PathNode> squares)
        {
            foreach (PathNode g in squares)
                HighLight(surface, g, Colors.Red);
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

        private void DrawGridLines(Panel surface, double StrokeWidth)
        {
            for (int x = 0; x <= (elementsX*Page.grid_size); x += Page.grid_size)
            {
                var l = new Line
                            {
                                X1 = x,
                                Y1 = 0,
                                X2 = x,
                                Y2 = Page.grid_size*elementsY,
                                Stroke = new SolidColorBrush(Colors.DarkGray),
                                StrokeThickness = StrokeWidth
                            };
                surface.Children.Add(l);
            }

            for (int y = 0; y <= (elementsY*Page.grid_size); y += Page.grid_size)
            {
                var l = new Line
                            {
                                X1 = 0,
                                Y1 = y,
                                X2 = Page.grid_size*elementsX,
                                Y2 = y,
                                Stroke = new SolidColorBrush(Colors.DarkGray),
                                StrokeThickness = StrokeWidth
                            };
                surface.Children.Add(l);
            }
        }

        public List<PathNode> CalculatePath(IGridShape start, IGridShape end)
        {
            var aStar = new AStarPathCalculator();
            return aStar.Calculate(start, end, this);
        }
    }
}