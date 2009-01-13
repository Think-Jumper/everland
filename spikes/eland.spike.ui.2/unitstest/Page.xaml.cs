using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace unitstest
{
    public partial class Page
    {
        private bool mouseDown;
        private Point clickPosition;

        private Rectangle selectedRectangle;

        public Page()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedRectangle = (Rectangle)sender;
            //clickPosition = e.GetPosition(null);
            //rect.CaptureMouse();
            mouseDown = true;
        }

        //private void rect_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (!mouseDown) return;

        //    var currEle = sender as FrameworkElement;
        //    // Retrieving the item's current x and y position
        //    var xPos = e.GetPosition(null).X - clickPosition.X;
        //    var yPos = e.GetPosition(null).Y - clickPosition.Y;

        //    // Re-position Element
        //    currEle.SetValue(Canvas.TopProperty, yPos + (double)currEle.GetValue(Canvas.TopProperty));
        //    currEle.SetValue(Canvas.LeftProperty, xPos + (double)currEle.GetValue(Canvas.LeftProperty));

        //    // Reset the new position value
        //    clickPosition = e.GetPosition(null);
        //}

        //private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!mouseDown) return;
        //    rect.ReleaseMouseCapture();
        //    mouseDown = false;
        //}

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

            var stb = new Storyboard();
            LayoutRoot.Resources.Add("stb_move", stb);

            var daX = new DoubleAnimation
                          {
                              From = ((double)selectedRectangle.GetValue(Canvas.LeftProperty)),
                              To = xPos,
                              Duration = new Duration(TimeSpan.FromSeconds(speed))
                          };

            var daY = new DoubleAnimation
                          {
                              From = ((double)selectedRectangle.GetValue(Canvas.TopProperty)),
                              To = yPos,
                              Duration = new Duration(TimeSpan.FromSeconds(speed))
                          };
            stb.Children.Add(daX);
            stb.Children.Add(daY);

            Storyboard.SetTarget(daX, selectedRectangle);
            Storyboard.SetTargetProperty(daX, new PropertyPath("(Canvas.Left)"));

            Storyboard.SetTarget(daY, selectedRectangle);
            Storyboard.SetTargetProperty(daY, new PropertyPath("(Canvas.Top)"));

            stb.Completed += stb_Completed;
            stb.Begin();

        }

        void stb_Completed(object sender, EventArgs e)
        {
            LayoutRoot.Resources.Remove("stb_move");
        }

   }
}
