namespace eland.utilities.Perlin
{
    public interface INoiseGenerator
    {
        float Frequency { get; set; }
        float Amplitude { get; set; }
        float Persistence { get; set; }
        int Octaves { get; set; }
        double Compute(int x, int y);
    }
}