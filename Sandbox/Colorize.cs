using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox
{
    public static class Colorize
    {

        public static int[] Normalize(int[] exposure, Properties p) {

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
            _ = Parallel.For(0, exposure.Length, i =>
            {
                double norm = Auxiliary.Normalize(exposure[i], min, max);
                int pixel = (int)(norm*255);
                if (norm > 1)
                {
                    //Console.WriteLine(norm);
                }
                
                exposure[i] = 255 << 24 | pixel << 16 | pixel << 8 | pixel << 0;

            });
            return exposure;
        }

        public static int[] Iterations(int[] exposure, Properties p)
        {
            Console.WriteLine(p.TimeStamp + " - Colorizing using an iterative algorithm.");
            _ = Parallel.For(0, exposure.Length, i =>
            {
                int pixel = exposure[i];
                exposure[i] = 255 << 24 | pixel << 16 | pixel << 8 | pixel << 0;
            });

            return exposure;
        }

        public static int[] IterationsCombine(int[] red, int[] green, int[] blue, Properties p)
        {
            int[] exposure = new int[red.Length];
            Array.Clear(exposure, 0, exposure.Length);

            Console.WriteLine(p.TimeStamp + " - Colorizing using an iterative algorithm.");
            _ = Parallel.For(0, red.Length, i =>
            {

                int r = red[i];
                int g = green[i];
                int b = blue[i];

                exposure[i] = 255 << 24 | 0 << 16 | 0 << 8 | 0 << 0; //set the alpha to 255

                //Red
                if (exposure[i] + r < 255){ exposure[i] += 0 << 24 | r << 16 | 0 << 8 | 0 << 0; }
                else { exposure[i] = 0 << 24 | 255 << 16 | 0 << 8 | 0 << 0; }
                //Green
                if (exposure[i] + g < 255){exposure[i] += 0 << 24 | 0 << 16 | g << 8 | 0 << 0;}
                else { exposure[i] = 0 << 24 | 0 << 16 | 255 << 8 | 0 << 0; }
                //Blue
                if (exposure[i] + b < 255){exposure[i] += 0 << 24 | 0 << 16 | 0 << 8 | b << 0;}
                else { exposure[i] = 0 << 24 | 0 << 16 | 0 << 8 | 255 << 0; }
            });

            return exposure;

        }

        //y=mx+b
        //public static int[] Slope(int[] exposure, Properties p)
        //{

        //    Parallel.For(0, exposure.Length, i =>
        //    {
        //    });
        //        return exposure;
        //}

        public static int[] Lerp(int[] exposure, Properties p)
        {

            Parallel.For(0, exposure.Length, i =>
            {
                int pixel = exposure[i];
                if (pixel > 0)
                {
                    double howMuch = Math.Clamp(pixel/Math.Sqrt(p.Highest),0,1);
                    int r = (int)Auxiliary.Lerp((double)p.From.R, (double)p.To.R, howMuch);
                    int g = (int)Auxiliary.Lerp((double)p.From.G, (double)p.To.G, howMuch);
                    int b = (int)Auxiliary.Lerp((double)p.From.B, (double)p.To.B, howMuch);
                    exposure[i] = 255 << 24 | r << 16 | g << 8 | b << 0;
                }
                else {
                exposure[i] = 255 << 24 | 0 << 16 | 0 << 8 | 0 << 0;
                }
            });

            return exposure;
        }

        public static int[] Log10Color(int[] exposure, Properties p)
        {
            Console.WriteLine(p.TimeStamp+" - Colorizing using a Log_10 algorithm.");
            string temp = p.Highest.ToString();
            int numZeroes = temp.Length-1;

            _ = Parallel.For(0, exposure.Length, i =>
            {
                if (exposure[i] > 0)
                {
                    double mLog = Math.Log(exposure[i], 10) / numZeroes; //dividend is the number of zeroes in the highest value in the histogram
                    int r = (int)Math.Clamp(Auxiliary.MapDouble(mLog, 0, 1, p.From.R, p.To.R), 0, 255);
                    int g = (int)Math.Clamp(Auxiliary.MapDouble(mLog, 0, 1, p.From.G, p.To.G), 0, 255);
                    int b = (int)Math.Clamp(Auxiliary.MapDouble(mLog, 0, 1, p.From.B, p.To.B), 0, 255);
                    exposure[i] = 255 << 24 | r << 16 | g << 8 | b << 0;
                }
                else
                {
                    exposure[i] = 255 << 24 | 0 << 16 | 0 << 8 | 0 << 0;
                }

            });
            
            return exposure;
        }

        public static int[] Ramp(int[] exposure, Properties p)
        {
            Console.WriteLine(p.TimeStamp + " - Colorizing using a ramping algorithm.");
            _ = Parallel.For(0, exposure.Length, i =>
            {
                double ramp = exposure[i] / (p.Highest / 11.0f);
                //float m = Auxiliary.mapFloat(p, 0, maxexposure, 0.5f, 0.70f);
                // blow out ultra bright regions
                if (ramp > 1)
                {
                    ramp = 1;
                }

                int r = (int)Math.Clamp(ramp * 255,0,255);
                int g = (int)Math.Clamp(ramp * 255, 0, 255);
                int b = (int)Math.Clamp(ramp * 255, 0, 255);

                exposure[i] = ((255 << 24) | (r << 16) | (g << 8) | (b << 0));

            });

            return exposure;
        }

        public static int[] Raw(int[] exposure, Properties p)
        {
            return exposure;
        }

    }

}
