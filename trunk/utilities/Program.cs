using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
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

            //Console.Write("Please enter Frequency (0.01) : ");
            //_noiseGenerator.Frequency = float.Parse(Console.ReadLine());
            //Console.Write("Please enter Amplitude (4.8) : ");
            //_noiseGenerator.Amplitude = float.Parse(Console.ReadLine());
            //Console.Write("Please enter Persistence (0.9) : ");
            //_noiseGenerator.Persistence = float.Parse(Console.ReadLine());
            //Console.Write("Please enter Octaves (2) : ");
            //_noiseGenerator.Octaves = int.Parse(Console.ReadLine());
            




            var terrain = new double[width * height];
            var normalisedTerrain = new double[width*height];
            var counter = 0;

            for(var y=0; y<height; y++)
            {
                for(var x=0; x<width; x++)
                {
                    terrain[counter++] = _noiseGenerator.Compute(x, y);
                }
            }

            var normalisedValues = Normalisation.CalculateInitialValues(terrain, 0, 255);
            var count = 0;

            var imageX = 0;
            var imageY = 0;
            var outputImage = new Bitmap(width, height);

            foreach (var value in terrain)
            {
                var normalisedValue = (int)Normalisation.Normalise(normalisedValues, value);
                normalisedTerrain[count++] = normalisedValue;

                outputImage.SetPixel(imageX++, imageY, Color.FromArgb(normalisedValue, normalisedValue, normalisedValue));
                if (imageX != outputImage.Width) continue;
                imageY++;
                imageX = 0;
            }

            double row = 0;
            const int gridSize = 1;
            var w = new World() {Name = "Default", Id = Guid.NewGuid()};

            for (var y = 0; y < height; y++)
            {
                row = (row + 2) % 2 == 0 ? 1 : 0;

                for (var x = 0; x < width; x++)
                {
                    var xx = ((x + 1) * (gridSize * 2));
                    if ((y + 2) % 2 != 0)
                    {
                        xx = ((x + 1) * (gridSize * 2)) - gridSize;
                    }

                    var yy = (int)((y * gridSize) * 0.5);
                    w.AddHex(new Hex()
                                {
                                    HexType = GetHexType(GetHeight(normalisedTerrain, xx, yy, width)), 
                                    Id = Guid.NewGuid(),
                                    X = xx,
                                    Y = yy
                                });
                                
                               

                    row += 2;
                }
            }

            Console.WriteLine("Total Hexes : " + w.TotalHexes);
            Console.WriteLine(GetCountForType(HexType.Ocean, w));
            Console.WriteLine(GetCountForType(HexType.Sea, w));
            Console.WriteLine(GetCountForType(HexType.Beach, w));
            Console.WriteLine(GetCountForType(HexType.Plain, w));
            Console.WriteLine(GetCountForType(HexType.Grass, w));
            Console.WriteLine(GetCountForType(HexType.Trees, w));
            Console.WriteLine(GetCountForType(HexType.Jungle, w));
            Console.WriteLine(GetCountForType(HexType.Hill, w));
            Console.WriteLine(GetCountForType(HexType.Mountain, w));

            
            outputImage.Save(@"c:\temp\map.jpg");
            Process.Start(@"c:\temp\map.jpg");


            Console.ReadKey();
            
        }

        private static string GetCountForType(HexType hexType, World w)
        {
            const string stats = "Total {0} : {1} ({2}%)";
            var totalHexesOfType = w.TotalHexesOfType(hexType);
            var percentage = totalHexesOfType/(double)w.TotalHexes * 100;

            return string.Format(stats, hexType, totalHexesOfType, percentage);
        }

       

        private static int GetHeight(double[] noise, int x, int y, int width)
        {
            return (int)noise[y*width + x];
        }

        private static HexType GetHexType(int height)
        {
            if (height < 10)
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