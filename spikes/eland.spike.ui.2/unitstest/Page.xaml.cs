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
        private readonly GridManager gridManager;
        private const double stroke_width = 0.5;
        public static int grid_size = 15;

        private GridSquare start;
        private GridSquare end;

        public Page()
        {
            InitializeComponent();
            gridManager = new GridManager();
        }

        public void Log(String text)
        {
            txtLog.Text += text;
            txtLog.Text += Environment.NewLine;
        }

        //private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    selectedRectangle = (Rectangle)sender;
        //    mouseDown = true;
        //}

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var xPos = e.GetPosition(cnvMain).X;
            var yPos = e.GetPosition(cnvMain).Y;

            var keys = Keyboard.Modifiers;
            var controlKey = (keys & ModifierKeys.Control) != 0;
            var shiftKey = (keys & ModifierKeys.Shift) != 0;

            if (controlKey)
            {
                gridManager.Block(cnvMain, (int)xPos, (int)yPos);
                return;
            }

            if (shiftKey)
            {
                var square = gridManager.HighlightGridSquare(cnvMain, (int) xPos, (int) yPos);
                var neighbours = gridManager.GetNeighbours(square);
                gridManager.HighlightGridSquares(cnvMain, neighbours);
                return;
            }

            if(start == null)
            {
                start = gridManager.HighlightGridSquare(cnvMain, (int)xPos, (int)yPos);
                return;
            }

            end = gridManager.HighlightGridSquare(cnvMain, (int)xPos, (int)yPos);

            // rough calculation of time spent
            // can't use Diagnostics.Stopwatch in SL2.0 :(

            var startTime = DateTime.Now;
            var path = gridManager.CalculatePath(start, end);
            var endTime = DateTime.Now;
            var span = new TimeSpan(endTime.Ticks - startTime.Ticks);

            txtLog.Text += Environment.NewLine;
            txtLog.Text += String.Format("Seconds taken : {0}", span.TotalSeconds);

            gridManager.HighlightGridSquares(cnvMain, path);
         
        }


        //private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var xPos = e.GetPosition(cnvMain).X;
        //    var yPos = e.GetPosition(cnvMain).Y;

        //    if (selectedRectangle == null || LayoutRoot.Resources.Contains("stb_move"))
        //    {
        //        var square = gridManager.HighlightGridSquare(cnvMain, (int)xPos, (int)yPos);

        //        var ne = gridManager.GetNeighbours(square);
        //        gridManager.HighlightGridSquares(cnvMain, ne);
        //        return;
        //    }

        //    if (mouseDown)
        //    {
        //        mouseDown = false;
        //        return;
        //    }

        //    var speed = slSpeed.Value;


        //    xPos = (Math.Round((xPos / 10)) * 10) + (stroke_width * 2);
        //    yPos = (Math.Round((yPos / 10)) * 10) + (stroke_width * 2);

        //    var stb = new Storyboard();
        //    LayoutRoot.Resources.Add("stb_move", stb);

        //    var daX = new DoubleAnimation
        //                  {
        //                      From = ((double)selectedRectangle.GetValue(Canvas.LeftProperty)),
        //                      To = xPos,
        //                      Duration = new Duration(TimeSpan.FromSeconds(speed)),
        //                      By = 10
        //                  };

        //    var daY = new DoubleAnimation
        //                  {
        //                      From = ((double)selectedRectangle.GetValue(Canvas.TopProperty)),
        //                      To = yPos,
        //                      Duration = new Duration(TimeSpan.FromSeconds(speed)),
        //                      By = 10
        //                  };
        //    stb.Children.Add(daX);
        //    stb.Children.Add(daY);

        //    Storyboard.SetTarget(daX, selectedRectangle);
        //    Storyboard.SetTargetProperty(daX, new PropertyPath("(Canvas.Left)"));

        //    Storyboard.SetTarget(daY, selectedRectangle);
        //    Storyboard.SetTargetProperty(daY, new PropertyPath("(Canvas.Top)"));

        //    stb.Completed += Animation_Completed;
        //    stb.Begin();
        //}

        //private void Animation_Completed(object sender, EventArgs e)
        //{
        //    LayoutRoot.Resources.Remove("stb_move");
        //}

        private void cnvMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gridManager.Draw(cnvMain, grid_size, stroke_width);
        }

    }

    public class GridSquare
    {
        public int Id { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Size { get; set; }
        public bool Blocked { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public bool Intersects(Point point)
        {
            return ((X1 < point.X && point.X < X2) && (Y1 < point.Y && point.Y < Y2));
        }

        public GridSquare(int x, int y, int size, int id, bool blocked, int row, int column)
        {
            X1 = (size * x);
            X2 = (size * (x + 1));
            Y1 = (size * y);
            Y2 = (size * (y + 1));
            Id = id;
            Size = size;
            Blocked = blocked;
            Row = row;
            Column = column;
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

            var id = 0;

            for (var y = 0; y < numSquaresY; y++)
            {
                for (var x = 0; x < numSquaresX; x++)
                {
                    var g = new GridSquare(x, y, gridSize, id++, false, y, x);

                    grid.Add(g);
                }
            }

            DrawGridLines(surface, strokeWidth);
        }

        public void Block(Canvas surface, int X, int Y)
        {
            foreach (var g in grid)
            {
                if (!g.Intersects(new Point(X, Y))) continue;
                g.Blocked = true;
                HighLight(surface, g, Colors.Blue);
                return;
            }
        }

        public GridSquare HighlightGridSquare(Canvas surface, int X, int Y)
        {
            foreach (var g in grid)
            {
                if (!g.Intersects(new Point(X, Y))) continue;
                HighLight(surface, g, Colors.Green);
                return g;
            }

            return null;
        }

        public void HighlightGridSquares(Canvas surface, List<GridSquare> squares)
        {
            foreach (var g in squares)
            {
                HighLight(surface, g, Colors.Red);
            }
        }

        public void HighlightGridSquares(Canvas surface, List<AStarPathCalculator.PathNode> squares)
        {
            var counter = 0;

            foreach (var g in squares)
                HighLight(surface, g, Colors.Red, counter++);
        }

        private static void HighLight(Panel surface, GridSquare rect, Color colour)
        {
            if (rect == null) return;
            var highlight = new Rectangle { Fill = new SolidColorBrush(colour) };
            highlight.SetValue(Canvas.TopProperty, (double)rect.Y1);
            highlight.SetValue(Canvas.LeftProperty, (double)rect.X1);
            highlight.Width = rect.X2 - rect.X1;
            highlight.Height = rect.Y2 - rect.Y1;

            surface.Children.Add(highlight);
        }

        private static void HighLight(Panel surface, AStarPathCalculator.PathNode rect, Color colour, int counter)
        {
            if (rect.Position == null) return;

            var highlight = new Rectangle { Fill = new SolidColorBrush(colour) };
            highlight.SetValue(Canvas.TopProperty, (double)rect.Position.Y1);
            highlight.SetValue(Canvas.LeftProperty, (double)rect.Position.X1);
            highlight.Width = rect.Position.X2 - rect.Position.X1;
            highlight.Height = rect.Position.Y2 - rect.Position.Y1;

            surface.Children.Add(highlight);
        }

        private void DrawGridLines(Panel surface, double StrokeWidth)
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

        public List<GridSquare> GetNeighbours(GridSquare centreGridSquare)
        {
            var neighbours = new List<GridSquare>();
            var currentColumn = centreGridSquare.Column;
            var currentRow = centreGridSquare.Row;

            neighbours.Add(grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn-1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn + 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn)).SingleOrDefault());
            neighbours.Add(grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn + 1)).SingleOrDefault());

            return neighbours;
        }

        public List<AStarPathCalculator.PathNode> CalculatePath(GridSquare start, GridSquare end)
        {
            var aStar = new AStarPathCalculator {NumberSquaresWidth = ((int) numSquaresX)};
            return aStar.Calculate(start, end, this);
        }

    }

    public interface IPathCalculator
    {
        List<AStarPathCalculator.PathNode> Calculate(GridSquare start, GridSquare end, GridManager gridManager);
    }

    public class AStarPathCalculator : IPathCalculator
    {
        private readonly List<PathNode> _closedList;
        private readonly List<PathNode> _openList;
        public int NumberSquaresWidth { get; set; }

        public AStarPathCalculator()
        {
            _openList = new List<PathNode>();
            _closedList = new List<PathNode>();
        }

        private PathNode CalculateFGH(PathNode currentNode, PathNode proposedNode, GridSquare endNode)
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

        private int CalculateCost(PathNode currentNode, PathNode proposedNode)
        {
            var idCost = Math.Abs(proposedNode.Position.Id - currentNode.Position.Id);
            if(idCost == 1 || idCost == NumberSquaresWidth)
            {
                return 10;
            }
            return 14;
        }


        public List<PathNode> Calculate(GridSquare start, GridSquare end, GridManager gridManager)
        {
            var startNode = new PathNode {Position = start};
            startNode = CalculateFGH(startNode, startNode, end);
            
            _openList.Add(startNode);

            while (true)
            {
                _openList.Sort();

                if (_openList.Count == 0)
                    return _closedList;

                var current = _openList[0]; 

                if (current.Position == end)
                    return _closedList;

                _closedList.Add(current);
                _openList.Clear();

                var neighbours = gridManager.GetNeighbours(current.Position);

                foreach (var g in neighbours)
                {
                    if (g == null) continue;

                    var c1 = _closedList.Where(x => x.Position == g).FirstOrDefault();

                    if (!g.Blocked && (c1 == null))
                    {
                        var newNode = new PathNode() { Position = g, Parent = current };
                        newNode = CalculateFGH(current, newNode, end);

                        if(!_openList.Contains(newNode))
                            _openList.Add(newNode);
                    }

                    if (!g.Blocked && (c1 != null))
                    {
                        var newNode = new PathNode() { Position = g, Parent = current };
                        newNode = CalculateFGH(current, newNode, end);

                        if (newNode.G < c1.G)
                        {
                            _openList.Remove(c1);
                            _openList.Add(newNode);
                        }
                    }
                }


            }

        }

        public class PathNode : IComparable<PathNode>
        {
            public GridSquare Position { get; set; }
            public PathNode Parent { get; set; }

            public int G { get; set; }
            public int H { get; set; }
            public int F
            {
                get { return G + H; }
            }

            public int CompareTo(PathNode other)
            {
                return F != other.F ? F.CompareTo(other.F) : G.CompareTo(other.G);
            }

            public override bool Equals(object obj)
            {
                return (Position.Id == ((PathNode) obj).Position.Id);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

      


    }
}
