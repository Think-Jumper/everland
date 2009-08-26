using System;

namespace eland.utilities.Perlin
{
    public class PerlinImproved
    {
        public float Frequency { get; set; }
        public float Amplitude { get; set; }
        public float Persistence { get; set; }
        public int Octaves { get; set; }

        static public double Noise(double x, double y, double z)
        {
            int xx = (int)Math.Floor(x) & 255,
                yy = (int)Math.Floor(y) & 255,
                zz = (int)Math.Floor(z) & 255;

            x -= Math.Floor(x);
            y -= Math.Floor(y);
            z -= Math.Floor(z);

            double u = Fade(x),
                v = Fade(y),
                w = Fade(z);

            int a = P[xx] + yy, 
                aa = P[a] + zz, 
                ab = P[a + 1] + zz, 
                b = P[xx + 1] + yy, 
                ba = P[b] + zz, 
                bb = P[b + 1] + zz; 

            return Lerp(w, Lerp(v, Lerp(u, Grad(P[aa], x, y, z), 
                                           Grad(P[ba], x - 1, y, z)),
                                   Lerp(u, Grad(P[ab], x, y - 1, z), 
                                           Grad(P[bb], x - 1, y - 1, z))),
                           Lerp(v, Lerp(u, Grad(P[aa + 1], x, y, z - 1), 
                                           Grad(P[ba + 1], x - 1, y, z - 1)),
                                   Lerp(u, Grad(P[ab + 1], x, y - 1, z - 1),
                                           Grad(P[bb + 1], x - 1, y - 1, z - 1))));
        }

        static double Fade(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        static double Lerp(double t, double a, double b)
        {
            return a + t * (b - a);
        }

        static double Grad(int hash, double x, double y, double z)
        {
            var h = hash & 15;
            double u = h < 8 ? x : y, v = h < 4 ? y : h == 12 || h == 14 ? x : z;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        public double Compute(float x, float y, float z)
        {
            double noise = 0;
            float amp = Amplitude;
            float freq = Frequency;
            for (int i = 0; i < Octaves; i++)
            {
                noise += Noise(x * freq, y * freq, z * freq) * amp;
                freq *= 2;                                // octave is the double of the previous frequency
                amp *= Persistence;
            }

            // Clamp and return the result
            if (noise < 0)
            {
                return 0;
            }
            return noise > 1 ? 1 : noise;
        }


        public PerlinImproved()
        {
            for (var i = 0; i < 256; i++) P[256 + i] = P[i] = Permutation[i];

            // Default values
            Frequency = 0.01f;
            Amplitude = 4.8f;
            Persistence = 0.9f;
            Octaves = 2;
        }


        private static readonly int[] P = new int[512];
        private static readonly int[] Permutation = { 151,160,137,91,90,15,
           131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
           190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
           88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
           77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
           102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
           135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
           5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
           223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
           129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
           251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
           49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
           138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
        };
    }
}
