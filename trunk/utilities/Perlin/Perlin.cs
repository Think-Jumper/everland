using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eland.utilities.Perlin
{
    public class Perlin
    {
        public static double Noise(int x, int y)
        {
            var n = x + y * 57;
            n = (n << 13) ^ n;
            return (1.0f - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0) ;
        }
        
        public static double PerlinNoise2D(int x, int y)
        {
            double total = 0.0;

            double frequency = .0625;    // USER ADJUSTABLE
            double persistence = .65;   // USER ADJUSTABLE
            double octaves = 8;         // USER ADJUSTABLE
            double amplitude = 1;       // USER ADJUSTABLE

            for (int lcv = 0; lcv < octaves; lcv++)
            {
                total = total + Smooth((int)(x * frequency), (int)(y * frequency)) * amplitude;
                frequency = frequency * 2;
                amplitude = amplitude * persistence;
            }

            return total;
        }

        public static double Interpolate(double a, double b, double x)
        {
            var ft = x * Math.PI;
            var f = (1 - Math.Cos(ft)) * 0.5;

            return a * (1 - f) + b * f;
        }

          public static double SmoothNoise(int x, int y)
          {
              var corners = (Noise(x - 1, y - 1) + Noise(x + 1, y - 1) + Noise(x - 1, y + 1) + Noise(x + 1, y + 1))/16;
              var sides = (Noise(x - 1, y) + Noise(x + 1, y) + Noise(x, y - 1) + Noise(x, y + 1))/8;
              var center = Noise(x, y)/4;
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
