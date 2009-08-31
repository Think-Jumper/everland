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
        public static int grid_size = 10;

        private IGridShape start;
        private IGridShape end;

        private bool initialised;

        public Page()
        {
            InitializeComponent();
            Initialise();
            initialised = true;
        }

        private void Initialise()
        {
            gridManager = new GridHexManager<GridHexFactory, GridHex>(new GridHexFactory());
        }

        private void Log(String text)
        {
            txtLog.Text += text;
            txtLog.Text += Environment.NewLine;
        }

        private void Redraw()
        {
            var randomise = false;
            randomise = (chkRandomise == null) ? randomise = true : chkRandomise.IsChecked.Value;
            gridManager.Draw(cnvMain, grid_size, stroke_width, randomise);
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

            //if (shiftKey)
            //{
            //    var square = gridManager.HighlightShape(cnvMain, (int) xPos, (int) yPos);
            //    var neighbours = gridManager.GetNeighbours(square);
            //    gridManager.HighlightGridShape(cnvMain, neighbours);
            //    return;
            //}

            if (start == null)
            {
                start = gridManager.HighlightShape(cnvMain, (int)xPos, (int)yPos);
                return;
            }

            end = gridManager.HighlightShape(cnvMain, (int)xPos, (int)yPos);

            // rough calculation of time spent
            // can't use Diagnostics.Stopwatch in SL2.0 :(

            var startTime = DateTime.Now;
            var path = gridManager.CalculatePath(start, end);
            var endTime = DateTime.Now;
            var span = new TimeSpan(endTime.Ticks - startTime.Ticks);

            Log(String.Format("Seconds taken : {0}", span.TotalSeconds) + Environment.NewLine);

            ////TODO: reverse iterate from end of list to draw actual path



            gridManager.HighlightGridShape(cnvMain, path);
         
        }

        private void cnvMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Redraw();
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if(initialised)
                Redraw();
        }



    }
}
