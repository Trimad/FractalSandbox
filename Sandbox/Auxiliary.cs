using System;
using System.Drawing;

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
    }
}
