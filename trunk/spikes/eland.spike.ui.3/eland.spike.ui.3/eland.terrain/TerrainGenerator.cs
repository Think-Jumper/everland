using System.Collections.Generic;
using eland.utilities;
using eland.utilities.TerrainGeneration.Noise;

namespace eland.terrain
{
    public enum TerrainGenerationMethod
    {
        Perlin,
        PerlinImproved
    }

    public class TerrainGenerator
    {

        public static IList<double> Generate(int width, int height, int z, TerrainGenerationMethod method)
        {
            INoiseGenerator noiseGenerator;

            if (method == TerrainGenerationMethod.Perlin)
                noiseGenerator = new Perlin();
            else
                noiseGenerator = new PerlinImproved();

            var noise = new double[width * height];
            var counter = 0;

            for(var y=0; y<height; y++)
            {
                for(var x=0; x<width; x++)
                {
                    noise[counter++] = noiseGenerator.Compute(x, y, z);
                }
            }

            var normalisedValues = Normalisation.CalculateInitialValues(noise, 0, 255);
            var normalisedNoise = new List<double>();

            foreach(var value in noise)
            {
                normalisedNoise.Add((int)Normalisation.Normalise(normalisedValues, value));
            }

            return normalisedNoise;
        }
    }
}