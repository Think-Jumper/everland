using System;
using System.Windows;
using System.Windows.Input;
using unitstest.Interfaces;

namespace unitstest
{
    public partial class Page
    {
        private IGridManager _gridManager;
        private const double StrokeWidth = 0.5;
        public static int GridSize = 10;

        private IGridShape _start;
        private IGridShape _end;

        private readonly bool _initialised;

        public Page()
        {
            InitializeComponent();
            Initialise();
            _initialised = true;
        }

        private void Initialise()
        {
            _gridManager = new GridHexManager<GridHexFactory, GridHex>(new GridHexFactory());
        }

        private void Log(String text)
        {
            txtLog.Text += text;
            txtLog.Text += Environment.NewLine;
        }

        private void Redraw()
        {
            var randomise = (chkRandomise == null) ? true : chkRandomise.IsChecked.Value;
            _gridManager.Draw(cnvMain, GridSize, StrokeWidth, randomise);
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
                _gridManager.Block(cnvMain, (int)xPos, (int)yPos);
                return;
            }

            if (shiftKey)
            {
                var square = _gridManager.HighlightShape(cnvMain, (int)xPos, (int)yPos);
                var neighbours = _gridManager.GetNeighbours(square);
                _gridManager.HighlightGridShape(cnvMain, neighbours);
                return;
            }

            if (_start == null)
            {
                _start = _gridManager.HighlightShape(cnvMain, (int)xPos, (int)yPos);
                return;
            }

            _end = _gridManager.HighlightShape(cnvMain, (int)xPos, (int)yPos);

            // rough calculation of time spent
            // can't use Diagnostics.Stopwatch in SL2.0 :(

            var startTime = DateTime.Now;
            var path = _gridManager.CalculatePath(_start, _end);
            var endTime = DateTime.Now;
            var span = new TimeSpan(endTime.Ticks - startTime.Ticks);

            Log(String.Format("Seconds taken : {0}", span.TotalSeconds) + Environment.NewLine);

            ////TODO: reverse iterate from _end of list to draw actual path



            _gridManager.HighlightGridShape(cnvMain, path);
         
        }

        private void cnvMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Redraw();
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if(_initialised)
                Redraw();
        }



    }
}
