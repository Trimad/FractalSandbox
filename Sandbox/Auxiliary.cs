using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Auxiliary
    {

        public static double RandomGaussian(double mean, int stdDev) {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
            public static float MapFloat(float value, float istart, float istop, float ostart, float ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }

        public static double MapDouble(double value, double istart, double istop, double ostart, double ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }
        public static double CustomLog(double n, double b)
        {
            return (Math.Log(n) / Math.Log(b));
        }

        public static double Lerp(double first, double second, double by)
        {
            return first * (1 - by) + second * by;
        }
        public static double Slope(Point p1, Point p2) {
            double m = ((double)p2.Y- (double)p1.Y)/((double)p2.X+ (double)p1.X);
            return m;
        }

        public static double Normalize(int value, int min, int max)
        {
            //if (min == 0 || max == 0)
            //{
            //    return value;
            //}
            return (value - min) / (max - min);
        }
        public static double Normalize(double value, double min, double max)
        {
            //if (min == 0 || max == 0)
            //{
            //    return value;
            //}
            return (value - min) / (max - min);
        }

        //Returns an array of values between 0 and 1.
        //public static double[] NormalizeArray(int[] exposure, double[] normalized)
        //{

        //    int min = int.MaxValue;
        //    int max = 0;

        //    Parallel.For(0, exposure.Length, i =>
        //    {
        //        if (exposure[i] < min) { min = exposure[i]; }
        //        if (exposure[i] > max) { max = exposure[i]; }
        //    });

        //    _ = Parallel.For(0, exposure.Length, i =>
        //    {
        //        double norm = Auxiliary.Normalize(exposure[i], min, max);
        //        normalized[i] = norm;

        //    });
        //    return normalized;
        //}
        public static double[] NormalizeArray(double[] arr)
        {
            double[] norm_arr = new double[arr.Length];
            double min = 9999;
            double max = 0;

            Parallel.For(0, arr.Length, i =>
            {
                if (arr[i] < min) { min = arr[i]; Console.WriteLine("min: "+min); }
                if (arr[i] > max) { max = arr[i]; Console.WriteLine("max: "+max); }
            });

            
            

            _ = Parallel.For(0, norm_arr.Length, i =>
            {
                double norm = Auxiliary.Normalize(arr[i], min, max);
                norm_arr[i] = norm;

            });
            return norm_arr;
        }

        //Returns an array of values between 0 and 255.
        public static void NormalizeArray(int[] exposure)
        {

            int min = int.MaxValue;
            int max = 0;
            //1. Find the minimum and maximum values.
            Parallel.For(0, exposure.Length, i =>
            {
                if (exposure[i] < min) { min = exposure[i]; }
                if (exposure[i] > max) { max = exposure[i]; }
            });
            //2. Normalize every pixel to a value between 0 and 1.
            //3. Multiply that normalized value by 255. 

            min = 0;
            max = 1000;
            _ = Parallel.For(0, exposure.Length, i =>
            {
                double norm = Auxiliary.Normalize(exposure[i], min, max);
                int pixel = (int)(norm * 255);

                exposure[i] = 255 << 24 | pixel << 16 | pixel << 8 | pixel << 0;

            });

        }
        public static double Distance(double x1, double y1, double x2, double y2) {
            double p1 = x2 - x1;
            double p2 = y2 - y1;
            double hypotenuse = Math.Sqrt((p1 * p1) + (p2 * p2));
            return hypotenuse;
        }
    }
}
