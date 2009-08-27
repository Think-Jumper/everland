namespace eland.utilities.TerrainGeneration.Noise
{
    public interface INoiseGenerator
    {
        float Frequency { get; set; }
        float Amplitude { get; set; }
        float Persistence { get; set; }
        int Octaves { get; set; }
        double Compute(float x, float y);
    }
}