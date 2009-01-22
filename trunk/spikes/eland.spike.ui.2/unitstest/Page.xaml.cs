using System;
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
        private bool mouseDown;
        private Rectangle selectedRectangle;
        private const double stroke_width = 0.5;

        public Page()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedRectangle = (Rectangle)sender;
            mouseDown = true;
        }

        private void DrawGrid()
        {
            for(var x = 0; x < cnvMain.ActualWidth; x+=10)
            {
                var l = new Line { X1 = x, Y1 = 0, X2 = x, Y2 = cnvMain.ActualHeight, Stroke = new SolidColorBrush(Colors.DarkGray), StrokeThickness = stroke_width };
                cnvMain.Children.Add(l);
            }

            for(var y=0; y<cnvMain.ActualHeight; y+=10)
            {
                var l = new Line { X1 = 0, Y1 = y, X2 = cnvMain.ActualWidth, Y2 = y, Stroke = new SolidColorBrush(Colors.DarkGray), StrokeThickness = stroke_width };
                cnvMain.Children.Add(l);
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DrawGrid();

            if (selectedRectangle == null || LayoutRoot.Resources.Contains("stb_move"))
                return;

            if (mouseDown)
            {
                mouseDown = false;
                return;
            }

            var speed = slSpeed.Value;

            var xPos = e.GetPosition(cnvMain).X;
            var yPos = e.GetPosition(cnvMain).Y;

            xPos = ((Math.Round(xPos / 10)) * 10) + stroke_width*2;
            yPos = ((Math.Round(yPos / 10)) * 10) + stroke_width*2;

            var stb = new Storyboard();
            LayoutRoot.Resources.Add("stb_move", stb);

            var daX = new DoubleAnimation
                          {
                              From = ((double) selectedRectangle.GetValue(Canvas.LeftProperty)),
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
            DrawGrid();

        }

   }
}
