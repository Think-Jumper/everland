using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace unitstest
{
    public partial class Page
    {
        private GridManager gMan;
        private bool mouseDown;
        private Rectangle selectedRectangle;
        private const double stroke_width = 0.5;

        public Page()
        {
            InitializeComponent();
            gMan = new GridManager();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedRectangle = (Rectangle)sender;
            mouseDown = true;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var xPos = (int)e.GetPosition(cnvMain).X;
            var yPos = (int)e.GetPosition(cnvMain).Y;

            if (selectedRectangle == null || !LayoutRoot.Resources.Contains("stb_move"))
                gMan.HighlightGridSquare(cnvMain, xPos, yPos);

            if (mouseDown)
            {
                mouseDown = false;
                return;
            }

            var speed = slSpeed.Value;

            xPos = ((Math.Round(xPos / 10)) * 10) + stroke_width * 2;
            yPos = ((Math.Round(yPos / 10)) * 10) + stroke_width * 2;

            var stb = new Storyboard();
            LayoutRoot.Resources.Add("stb_move", stb);

            var daX = new DoubleAnimation
                          {
                              From = ((double)selectedRectangle.GetValue(Canvas.LeftProperty)),
                              To = xPos,
                              Duration = new Duration(TimeSpan.FromSeconds(speed)),
                              By = 10
                          };

            var daY = new DoubleAnimation
                          {
                              From = ((double)selectedRectangle.GetValue(Canvas.TopProperty)),
                              To = yPos,
                              Duration = new Duration(TimeSpan.FromSeconds(speed)),
                              By = 10
                          };
            stb.Children.Add(daX);
            stb.Children.Add(daY);

            Storyboard.SetTarget(daX, selectedRectangle);
            Storyboard.SetTargetProperty(daX, new PropertyPath("(Canvas.Left)"));

            Storyboard.SetTarget(daY, selectedRectangle);
            Storyboard.SetTargetProperty(daY, new PropertyPath("(Canvas.Top)"));

            stb.Completed += Animation_Completed;
            stb.Begin();
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            LayoutRoot.Resources.Remove("stb_move");
        }

        private void cnvMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gMan.Draw(cnvMain, 10, 0.5);

        }

    }

    public class GridSquare
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public bool Visited { get; set; }
        public bool Blocked { get; set; }

        public bool Intersects(Point point)
        {
            return ((X1 < point.X && point.X < X2) && (Y1 < point.Y && point.Y < Y2));
        }

        public GridSquare(int X, int Y, int Size)
        {
            X1 = (Size * X);
            X2 = (Size * (X + 1));
            Y1 = (Size * Y);
            Y2 = (Size * (Y + 1));
        }

        public void HighLight(Canvas surface)
        {
            var highlight = new Rectangle {Fill = new SolidColorBrush(Colors.Green)};
            highlight.SetValue(Canvas.TopProperty, (double)Y1);
            highlight.SetValue(Canvas.LeftProperty, (double)X1);
            highlight.Width = X2 - X1;
            highlight.Height = Y2 - Y1;

            surface.Children.Add(highlight);
        }

    }

    public class GridManager
    {
        private List<GridSquare> grid;
        private double numSquaresX;
        private double numSquaresY;
        private int GridSquareSize;

        public void Draw(Canvas surface, int gridSize, double strokeWidth)
        {
            numSquaresX = Math.Round(surface.ActualWidth / gridSize);
            numSquaresY = Math.Round(surface.ActualHeight / gridSize);

            GridSquareSize = gridSize;

            grid = new List<GridSquare>();

            for (var x = 0; x < numSquaresX; x++)
            {
                for (var y = 0; y < numSquaresY; y++)
                {
                    var g = new GridSquare(x, y, gridSize);

                    grid.Add(g);
                }
            }

            DrawGridLines(surface, strokeWidth);
        }

        public void HighlightGridSquare(Canvas surface, int X, int Y)
        {
            foreach (var g in grid)
            {
                if (g.Intersects(new Point(X, Y)))
                    g.HighLight(surface);

            }
        }

        private void DrawGridLines(Canvas surface, double StrokeWidth)
        {
            for (var x = 0; x < (numSquaresX * GridSquareSize); x += GridSquareSize)
            {
                var l = new Line { X1 = x, Y1 = 0, X2 = x, Y2 = GridSquareSize * numSquaresY, Stroke = new SolidColorBrush(Colors.DarkGray), StrokeThickness = StrokeWidth };
                surface.Children.Add(l);
            }

            for (var y = 0; y < (numSquaresY * GridSquareSize); y += GridSquareSize)
            {
                var l = new Line { X1 = 0, Y1 = y, X2 = GridSquareSize * numSquaresX, Y2 = y, Stroke = new SolidColorBrush(Colors.DarkGray), StrokeThickness = StrokeWidth };
                surface.Children.Add(l);
            }
        }

    }
}
