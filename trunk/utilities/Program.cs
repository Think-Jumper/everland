using System.Drawing;
using eland.utilities.Perlin;

namespace eland.utilities
{
    class Program
    {
        static void Main()
        {
            Run();
        }

        private static void Run()
        {
            var terrain = new double[40000];
            var counter = 0;
            var outputImage = new Bitmap(200,200);
            var perlin = new PerlinImproved();

            for(var y=0; y<200; y++)
            {
                for(var x=0; x<200; x++)
                {
                    terrain[counter++] = perlin.Compute(x, y, 1);
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