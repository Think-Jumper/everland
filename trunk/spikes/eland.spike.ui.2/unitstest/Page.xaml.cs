using System;
using System.Windows;
using System.Windows.Input;
using unitstest.Interfaces;

namespace unitstest
{
    public partial class Page
    {
        private IGridManager gridManager;
        private const double stroke_width = 0.5;
        public static int grid_size = 8;

        private IGridShape start;
        private IGridShape end;

        public Page()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            gridManager = new GridManager<GridSquareFactory, GridSquare>(new GridSquareFactory());
        }

        private void Log(String text)
        {
            txtLog.Text += text;
            txtLog.Text += Environment.NewLine;
        }

        private void Redraw()
        {
            gridManager.Draw(cnvMain, grid_size, stroke_width, true);
        }

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

            Log(String.Format("Seconds taken : {0}", span.TotalSeconds));

            //TODO: reverse iterate from end of list to draw actual path

            gridManager.HighlightGridSquares(cnvMain, path);
         
        }

        private void cnvMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Redraw();
        }



    }
}
