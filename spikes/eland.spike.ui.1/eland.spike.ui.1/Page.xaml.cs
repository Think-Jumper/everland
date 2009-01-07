using System;
using System.Net;
using System.Windows;
using SilverlightSpike1.Helpers;

namespace eland.spike.ui._1
{
    public partial class Page
    {
        public Page()
        {
            InitializeComponent();
        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            var searchURL = string.Format("http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}", txtSearchQuery.Text.Trim());

            var googleService = new WebClient();
            googleService.OpenReadCompleted += googleService_OpenReadCompleted;
            googleService.OpenReadAsync(new Uri(searchURL));
        }

        private void googleService_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;

            var result = e.Result;
            var results = JsonHelper<GoogleResponseWrapper>.ConvertJsonStringToObject(result);

            DisplaySearchResults(results.responseData);
        }

        public void DisplaySearchResults(GoogleSearchResults results)
        {
            grResults.SelectedIndex = -1;
            grResults.ItemsSource = results.results;
        }


    }

    public class GoogleResponseWrapper
    {
        public GoogleSearchResults responseData;
        public string responseDetails;
        public int responseStatus;
    }

    public class GoogleSearchResults
    {
        public GoogleResult[] results;
    }

    public class GoogleResult
    {
        public string GsearchResultClass { get; set; }
        public string unescapedUrl { get; set; }
        public string url { get; set; }
        public string visibleUrl { get; set; }
        public string cacheUrl { get; set; }
        public string title { get; set; }
        public string titleNoFormatting { get; set; }
        public string content { get; set; }
    }
}
