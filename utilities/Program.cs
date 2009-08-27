using System;
using System.Drawing;
using eland.utilities.TerrainGeneration.Noise;

namespace eland.utilities
{
    class Program
    {
        private static INoiseGenerator _noiseGenerator;

        static void Main()
        {
            Run();
        }

        private static void Run()
        {
            Console.Write("Please enter width in pixels : ");
            var width = int.Parse(Console.ReadLine());
            Console.Write("Please enter height in pixels : ");
            var height = int.Parse(Console.ReadLine());
            Console.Write("Press 1 to use Perlin, 2 to use Improved Perlin : ");
            
            if(int.Parse(Console.ReadLine()) == 1)
                _noiseGenerator = new Perlin();
            else
                _noiseGenerator = new PerlinImproved();

            var terrain = new double[width * height];
            var counter = 0;
            var outputImage = new Bitmap(width, height);

            for(var y=0; y<height; y++)
            {
                for(var x=0; x<width; x++)
                {
                    terrain[counter++] = _noiseGenerator.Compute(x, y);
                }
            }

            var normalisedValues = Normalisation.CalculateInitialValues(terrain, 0, 255);
            var xx = 0;
            var yy = 0;

            foreach(var value in terrain)
            {
                var normalisedValue = (int)Normalisation.Normalise(normalisedValues, value);
                outputImage.SetPixel(xx++, yy, Color.FromArgb(normalisedValue, normalisedValue, normalisedValue));
                if (xx != outputImage.Width) continue;
                yy++;
                xx = 0;
            }

            outputImage.Save(@"perlin.png");
        }
    }
}