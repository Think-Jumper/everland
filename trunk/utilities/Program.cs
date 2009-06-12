using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace eland.utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static float[] input;
        private const string inputFile = @"c:\WorldMapColoured.png";
        private const string outputFileName = @"c:\Output.png";
        private static readonly Bitmap outputImage = new Bitmap(1420, 655);

        private static void Run()
        {
            ParseInputImage();
            OutputUniqueValues(CreateOutputImage(CalculateNormalValues(input)));

            Console.WriteLine("Press 1 to create Hexes.");
           // if Console.ReadKey().Key

        }

        private static void OutputUniqueValues(IList<float> uniqueValues)
        {
            uniqueValues = Normalisation.GetDistinctValues(uniqueValues);
            foreach(var value in uniqueValues)
                Console.WriteLine(value);

        }

        private static Normalisation.NormalisationValues CalculateNormalValues(float[] values)
        {
            return Normalisation.CalculateInitialValues(values, 0, 255);
        }

        private static void ParseInputImage()
        {
            var inputImage = new Bitmap(Image.FromFile(inputFile));
            var outputCount = 0;
            input = new float[inputImage.Width * inputImage.Height];

            for (var y = 0; y < inputImage.Height; y++)
            {
                for (var x = 0; x < inputImage.Width; x++)
                {
                    input[outputCount++] = (float)(inputImage.GetPixel(x, y).R + inputImage.GetPixel(x, y).G + inputImage.GetPixel(x, y).B) / 3;
                }
            }



        }

        private static IList<float> CreateOutputImage(Normalisation.NormalisationValues initialValues)
        {
            var x = 0;
            var y = 0;
            var normalisedValues = new List<float>();

            foreach(var value in input)
            {
                var normalisedValue = Normalisation.Normalise(initialValues, value);

                normalisedValues.Add(normalisedValue);

                outputImage.SetPixel(x++, y, Color.FromArgb((int)normalisedValue, (int)normalisedValue, (int)normalisedValue));

                if (x != 1420) continue;
                y++;
                x = 0;
            }

            outputImage.Save(outputFileName);

            return normalisedValues;
        }

        private static void CreateHexes()
        {
            
        }





        //public byte[] Render()
        //{
        //    Int32 left = 0; // should be parameratized
        //    Int32 top = 0; // should be parameratized

        //    Int32 xMagnitude = (sideLength * 2 * (columns + 1));
        //    Int32 yMagnitude = (Int32)(sideLength * Math.Sqrt(3) * (rows + 1));

        //    ImageSurface destination = new Cairo.ImageSurface(Cairo.Format.Argb32, xMagnitude, yMagnitude); // not sure on Format
        //    Context context = new Context(destination);

        //    PointD upperLeftPoint = new PointD(sideLength * 2, sideLength * 2); // just to ensure first hex fits is on the page, not meaningful code

        //    for (int i = 0; i < rows; i++)
        //    {
        //        upperLeftPoint = new PointD((sideLength / 2) + left, ((Int32)(i * Math.Sqrt(3) * sideLength)) + top);
        //        Double rowTop = upperLeftPoint.Y;

        //        for (int j = 1; j <= columns; j++)
        //        {

        //            PointD[] hexPoints = getHexagonPoints(upperLeftPoint, sideLength);

        //            if (isEven(j))
        //            {
        //                upperLeftPoint = new PointD(hexPoints[5].X + (2 * sideLength), rowTop);
        //            }
        //            else
        //            {
        //                upperLeftPoint = new PointD(hexPoints[2].X, hexPoints[2].Y);
        //            }

        //            context.LineWidth = 1;
        //            context.SetSourceRGB(0, 0, 0);

        //            context.MoveTo(hexPoints[0]);
        //            context.LineTo(hexPoints[1]);
        //            context.LineTo(hexPoints[2]);
        //            context.LineTo(hexPoints[3]);
        //            context.LineTo(hexPoints[4]);
        //            context.LineTo(hexPoints[5]);

        //            context.ClosePath();
        //            context.Stroke();

        //        }
        //    }


        //    String file = workingDirectory;
        //    byte[] buffer;

        //    try
        //    {
        //        file += Guid.NewGuid().ToString() + ".png";
        //        destination.WriteToPng(file);

        //        using (FileStream fs = new FileStream(file, FileMode.Open))
        //        {
        //            buffer = new byte[fs.Length];
        //            fs.Read(buffer, 0, (Int32)fs.Length);
        //        }
        //    }
        //    finally
        //    {

        //        if (File.Exists(file))
        //            File.Delete(file);
        //        else
        //            throw new Exception();

        //    }
        //    return buffer;
        //}

        static Point[] getHexagonPoints(Point upperLeftPoint, Int32 sideLength)
        {
            var p2 = new Point(upperLeftPoint.X + sideLength, upperLeftPoint.Y);
            var p3 = new Point(upperLeftPoint.X + ((3 * sideLength) / 2), (int)(upperLeftPoint.Y + ((Math.Sqrt(3) * sideLength) / 2)));
            var p4 = new Point(p2.X, (int)(upperLeftPoint.Y + ((Math.Sqrt(3) * sideLength))));
            var p5 = new Point(upperLeftPoint.X, p4.Y);
            var p6 = new Point(upperLeftPoint.X - (sideLength / 2), p3.Y);

            var allPoints = new[] { upperLeftPoint, p2, p3, p4, p5, p6 };

            return allPoints;
        }

        static Boolean isEven(Int32 value)
        {
            return value % 2 == 0;
        }

    }
}