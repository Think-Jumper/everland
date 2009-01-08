using System.Windows;

namespace eland.spike.ui._1
{
    public partial class SearchResultDisplay
    {
        public SearchResultDisplay()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
