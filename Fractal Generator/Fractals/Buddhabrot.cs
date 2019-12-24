using System;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    public class Buddhabrot : Fractal
    {
        public Buddhabrot(int[] pix, double[,,] dom, Settings settings)
        {
            name = "Buddhabrot";
            pixels = pix;
            domain = dom;
            properties = settings;
        }

        private static int highest = 0;
        public override int[] Render(Settings p)
        {

            while(highest < properties.Highest)
            { 
                Plot(properties.Width, properties.Height);
                Console.WriteLine(highest);
            }
            return pixels;
        }

        private void Plot(int w, int h)
        {

            _ = Parallel.For(0, w, x =>
              {

                  for (int y = 0; y < h; y++)
                  {

                      double r = domain[x, y, 0];
                      double i = domain[x, y, 1];
                      double rx = Auxiliary.Random();
                      double ry = Auxiliary.Random();
                      r += rx;
                      i += ry;

                      if (Iterate(r, i, w, h, false))
                      {
                          Iterate(r, i, w, h, true);
                      }

                  }
              });

        }

        //Iterate the Mandelbrot and return TRUE if the point escapes
        //Also handle the drawing of the exit points
        private bool Iterate(double x0, double y0, int w, int h, bool drawIt)
        {
            double x = 0;
            double y = 0;
            double xnew, ynew;

            for (int i = 0; i < properties.Bailout; i++)
            {

                ynew = (x * x - y * y) + x0;
                xnew = (2 * x * y) + y0;

                if (drawIt && (i > properties.Cutoff))
                {

                    int ix = (int)Auxiliary.MapDouble(xnew, domain[0, 0, 0], domain[w - 1, 0, 0], 0, w - 1);
                    int iy = (int)Auxiliary.MapDouble(ynew, domain[0, 0, 1], domain[0, h - 1, 1], 0, h - 1);

                    if (ix >= 0 && iy >= 0 && iy < h && ix < w)
                    {
                        // rotate and expose point
                        int index = iy * w + ix;
                        pixels[index]++;
                        if (highest < pixels[index]) { highest = pixels[index]; }
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