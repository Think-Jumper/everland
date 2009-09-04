using System.Collections.Generic;
using System.Linq;

namespace eland.utilities
{
    public class Normalisation
    {
        public struct NormalisationValues
        {
            public float a, b, c, d, r;
        }

        public static NormalisationValues CalculateInitialValues(float[] values, float min, float max)
        {
            return new NormalisationValues {a = min, b = max, d = values.Max(), c = values.Min()};
        }

        public static NormalisationValues CalculateInitialValues(double[] values, float min, float max)
        {
            return new NormalisationValues { a = min, b = max, d = (float)values.Max(), c = (float)values.Min() };
        }

        public static float Normalise(NormalisationValues normalVals, float value)
        {
            return (value - normalVals.c) * ((normalVals.b - normalVals.a) / (normalVals.d - normalVals.c)) + normalVals.a;
        }

        public static double Normalise(NormalisationValues normalVals, double value)
        {
            return (value - normalVals.c) * ((normalVals.b - normalVals.a) / (normalVals.d - normalVals.c)) + normalVals.a;
        }

        public static List<float> GetDistinctValues(IList<float> values)
        {
            return values.Distinct().ToList();
        }
             
    }
}