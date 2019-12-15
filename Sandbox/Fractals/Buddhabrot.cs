using System;
using System.Threading.Tasks;

namespace Sandbox.Fractals
{
    public class Buddhabrot : Fractal
    {
        public Buddhabrot(int[] p, double[,,] d)
        {
            name = "Buddhabrot";
            pixels = p;
            domain = d;
        }

        private static int highestExposure = 0;
        public override int[] Render(Properties p)
        {

            while (highestExposure < p.Highest)
            {
                for (int x = 0; x < p.Width; x++)
            {
                for (int y = 0; y < p.Height; y++)
                {
                    if (Iterate(x, y, p, false))
                    {
                        Iterate(x, y, p, true);
                    }
                }
            }
            }
            highestExposure = 0;
            return pixels;
        }

        //Iterate the Mandelbrot and return TRUE if the point escapes
        //Also handle the drawing of the exit points
        private bool Iterate(int x, int y, Properties p, bool drawIt)
        {

            Complex z = new Complex(0, 0);
            Complex zNew;
            Complex c = new Complex(domain[x, y, 0], domain[x, y, 1]);

            for (int i = 0; i < p.Bailout; i++)
            {
                z.Square();
                z.Add(c);
                zNew = new Complex(z.a, z.b);

                if (drawIt && i >= p.Cutoff)
                {
                    double zx = Auxiliary.MapDouble(zNew.a, domain[0,0,0], domain[p.Width - 1, 0, 0], 0, p.Width-1);
                    int clampZX = Math.Clamp((int)zx, 0, p.Width - 1);

                    double zy = Auxiliary.MapDouble(zNew.b, domain[0, 0, 1], domain[0, p.Height - 1, 1], 0, p.Height-1);
                    int clampZY = Math.Clamp((int)zy, 0, p.Height - 1);
                    
                    int index = clampZY * p.Width + clampZX;
                    
                    pixels[index]++;
                    if (highestExposure < pixels[index]) { highestExposure = pixels[index]; }
                }
                if (zNew.Magnitude() > 2.0)
                {
                    // escapes
                    return true;
                }
                z = zNew;

            }
            //does not escape
            return false;
        }
    }
}
