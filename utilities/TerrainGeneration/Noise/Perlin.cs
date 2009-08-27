using System;

namespace eland.utilities.TerrainGeneration.Noise
{
    public class Perlin : INoiseGenerator
    {
        public float Frequency { get; set; }
        public float Amplitude { get; set; }
        public float Persistence { get; set; }
        public int Octaves { get; set; }


        private static double Noise(int x, int y)
        {
            var n = x + y * 57;
            n = (n << 13) ^ n;
            return (1.0f - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        public Perlin()
        {
            Frequency = 0.0625f;
            Amplitude = 1f;
            Persistence = 0.65f;
            Octaves = 8;
        }

        public double Compute(float x, float y)
        {
            var total = 0.0;
            var amp = Amplitude;
            var freq = Frequency;
            
            for (var lcv = 0; lcv < Octaves; lcv++)
            {
                total = total + Smooth((int)(x * freq), (int)(y * freq)) * amp;
                freq = freq * 2;
                amp = amp * Persistence;
            }

            return total;
        }

        private static double Interpolate(double a, double b, double x)
        {
            var ft = x * Math.PI;
            var f = (1 - Math.Cos(ft)) * 0.5;

            return a * (1 - f) + b * f;
        }

        private static double SmoothNoise(int x, int y)
        {
            var corners = (Noise(x - 1, y - 1) + Noise(x + 1, y - 1) + Noise(x - 1, y + 1) + Noise(x + 1, y + 1)) / 16;
            var sides = (Noise(x - 1, y) + Noise(x + 1, y) + Noise(x, y - 1) + Noise(x, y + 1)) / 8;
            var center = Noise(x, y) / 4;
            return corners + sides + center;
        }

        private static double Smooth(double x, double y)
        {
            var n1 = SmoothNoise((int)x, (int)y);
            var n2 = SmoothNoise((int)x + 1, (int)y);
            var n3 = SmoothNoise((int)x, (int)y + 1);
            var n4 = SmoothNoise((int)x + 1, (int)y + 1);

            var i1 = Interpolate(n1, n2, x - (int)x);
            var i2 = Interpolate(n3, n4, x - (int)x);

            return Interpolate(i1, i2, y - (int)y);
        }
    }
}