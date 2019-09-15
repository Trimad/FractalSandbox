using System;

namespace Sandbox.BuddhabrotOld
{
    public static class BuddhabrotST
    {
        public static int[] Render(int w, int h, double[] area, double[] center)
        {

            int[] pixels = new int[w * h];
            int passes = 50;
            for (int i = 0; i < passes; i++)
            {
                double progress = Auxiliary.MapDouble(i, 0, passes, 0, 100);
                Console.WriteLine(progress.ToString("#.##") + "% finished rendering.");
                Plot(w, h, area, center, pixels);
            }

            return pixels;
        }

        static void Plot(int w, int h, double[] area, double[] center, int[] pixels)
        {

            Random rand = new Random(); //reuse this if you are generating many

            for (int x = 0; x < w; x++)
            {

                for (int y = 0; y < h; y++)
                {

                    double x0 = Auxiliary.MapDouble(x, 0, w, center[0] + (-area[0]), center[0] + area[0]);
                    double y0 = Auxiliary.MapDouble(y, 0, h, center[1] + (-area[1]), center[1] + area[1]);
                    double xd = -0.5 + rand.NextDouble();
                    double yd = -0.5 + rand.NextDouble();
                    x0 += xd;
                    y0 += yd;

                    if (Iterate(x0, y0, w, h, false, pixels))
                    {
                        Iterate(x0, y0, w, h, true, pixels);
                    }

                }
            }

        }

        //Iterate the Mandelbrot and return TRUE if the point escapes
        //Also handle the drawing of the exit points
        static private bool Iterate(double x0, double y0, int w, int h, bool drawIt, int[] exposure)
        {
            int BAILOUT = 1500;
            int CUTOFF = 3;
            double x = 0;
            double y = 0;
            double xnew, ynew;

            double aspectRatio = (double)w / (double)h;
            double scaledX1 = -2.0f * aspectRatio;
            double scaledX2 = 2.0f * aspectRatio;
            double scaledY1 = -2.0f;
            double scaledY2 = 2.0f;

            for (int i = 0; i < BAILOUT; i++)
            {

                ynew = (x * x - y * y) + x0;
                xnew = (2 * x * y) + y0;

                if (drawIt && (i > CUTOFF))
                {

                    int ix = (int)Auxiliary.MapDouble(xnew, scaledX1, scaledX2, 0, w);
                    int iy = (int)Auxiliary.MapDouble(ynew, scaledY1, scaledY2, 0, h);

                    if (ix >= 0 && iy >= 0 && iy < h && ix < w)
                    {
                        // rotate and expose point
                        exposure[iy * w + ix]++;
                    }
                }
                if ((xnew * xnew + ynew * ynew) > 4)
                {
                    // escapes
                    return true;
                }
                y = xnew;
                x = ynew;
            }
            //does not escape
            return false;
        }


    }
}
