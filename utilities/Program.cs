using System;
using System.Collections.Generic;
using System.Drawing;
using eland.api;
using eland.api.Interfaces;
using eland.model;
using eland.model.Enums;
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

            //foreach(var value in terrain)
            //{
            //    var normalisedValue = (int)Normalisation.Normalise(normalisedValues, value);
            //    outputImage.SetPixel(xx++, yy, Color.FromArgb(normalisedValue, normalisedValue, normalisedValue));
            //    if (xx != outputImage.Width) continue;
            //    yy++;
            //    xx = 0;
            //}

            double row = 0;
            const int gridSize = 1;
            var w = new World() {Name = "Default", Id = Guid.NewGuid()};

            for (var y = 0; y < height; y++)
            {
                row = (row + 2) % 2 == 0 ? 1 : 0;

                for (var x = 0; x < width; x++)
                {
                    xx = ((x + 1) * (gridSize * 2));
                    if ((y + 2) % 2 != 0)
                    {
                        xx = ((x + 1) * (gridSize * 2)) - gridSize;
                    }

                    yy = (int)((y * gridSize) * 0.5);
                    w.AddHex(new Hex()
                                {
                                    HexType = GetHexType(GetHeight(terrain, xx, yy, width)), 
                                    Id = Guid.NewGuid(),
                                    X = xx,
                                    Y = yy
                                });
                                
                               

                    row += 2;
                }

               // column += 0.5;
            }

            var dataContext = IoC.Resolve<IDataContext>();

            using(var tran = dataContext.WorldRepository.Session.BeginTransaction())
            {
                dataContext.WorldRepository.Save(w);
                tran.Commit();
            }



            //outputImage.Save(@"c:\perlin.png");
        }

        private static int GetHeight(double[] noise, int x, int y, int width)
        {
            return (int)noise[y*width + x];
        }

        private static HexType GetHexType(int height)
        {

            if (height < 30)
                return HexType.Ocean;
            if (height < 50)
                return HexType.Sea;
            if (height < 80)
                return HexType.Beach;
            if (height < 125)
                return HexType.Plain;
            if (height < 145)
                return HexType.Grass;
            if (height < 175)
                return HexType.Trees;
            if (height < 200)
                return HexType.Jungle;
            return height < 225 ? HexType.Hill : HexType.Mountain;
        }
    }
}