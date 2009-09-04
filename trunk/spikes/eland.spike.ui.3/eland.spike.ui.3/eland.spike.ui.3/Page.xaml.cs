using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using eland.terrain;
using unitstest;

namespace eland.spike.ui._3
{
    public partial class Page
    {
        private IList<double> _noise;

        public Page()
        {
            InitializeComponent();
        }

        private void btnGenerateNoise_Click(object sender, RoutedEventArgs e)
        {
            GenerateNoise();
        }

        private void btnGenerateHexes_Click(object sender, RoutedEventArgs e)
        {
            imgMain.Visibility = Visibility.Collapsed;
            //Canvas.SetZIndex(imgMain, 0);
            //Canvas.SetZIndex(cnvMain, 1);
            GenerateHexes();
        }

        private void GenerateHexes()
        {
            var gridManager = new GridHexManager<GridHexFactory, GridHex>(new GridHexFactory());

            gridManager.Draw(cnvMain, 2, 0.1, false, _noise);


        }

        private void GenerateNoise()
        {
            var z = new Random();
            var width = (int)cnvMain.ActualWidth;
            var height = (int)cnvMain.ActualHeight;
            _noise = TerrainGenerator.Generate(width, height, z.Next(0, 100000), TerrainGenerationMethod.PerlinImproved);
            int xx = 0, yy = 0;
            var outputImage = new BitmapImage();
            var encodedImage = new EditableImage(width, height);

            foreach (var noiseValue in _noise)
            {
                var color = Color.FromArgb(255, (byte)noiseValue, (byte)noiseValue, (byte)noiseValue);

                encodedImage.SetPixel(xx++, yy, color);

                if (xx != width) continue;
                yy++;
                xx = 0;
            }

            outputImage.SetSource(encodedImage.GetStream());
            imgMain.Source = outputImage;
        }

        private void slScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CanvasScaleTransform.ScaleX = slScale.Value;
            CanvasScaleTransform.ScaleY = slScale.Value;
        }
    }
}
