using System;
using System.Threading.Tasks;

namespace Sandbox.Fractals
{
    public class Test : Fractal
    {
        public Test(int[] pix, double[,,] dom, Properties pro)
        {
            name = "Buddhabrot";
            pixels = pix;
            domain = dom;
            properties = pro;
        }

        private static int highestExposure = 0;
        public override int[] Render(Properties p)
        {

            //while (highestExposure < p.Highest)
            //{
                for (int x = 0; x < p.Width; x++)
            {
                for (int y = 0; y < p.Height; y++)
                {
                    if (Iterate(x, y, properties.Width, properties.Height, false))
                    {
                        Iterate(x, y, properties.Width, properties.Height, true);
                    }
                }
            }
            //}
            //highestExposure = 0;
            return pixels;
        }

        //Iterate the Mandelbrot and return TRUE if the point escapes
        private bool Iterate(int x, int y, int W, int H, bool drawIt)
        {

            Complex z = new Complex(0, 0);
            Complex zNew;
            Complex c = new Complex(domain[x, y, 0], domain[x, y, 1]);

            for (int i = 0; i < properties.Bailout; i++)
            {
                z.Square();
                z.Add(c);
                zNew = new Complex(z.a, z.b);

                if (drawIt && i >= properties.Cutoff)
                {
                    int zx = Math.Clamp((int)
                        Auxiliary.MapDouble(zNew.a, domain[0, 0, 0], domain[0, W, 0], 0, W)
                        , 0, W-1);
                    if (zx > W - 1 || zx < 0) { Console.WriteLine(zx); }
                    int zy = Math.Clamp((int)
                        Auxiliary.MapDouble(zNew.b, domain[0, 0, 1], domain[0, H, 1], 0, H)
                        , 0, H-1);

                    int index = zy * W-1 + zx;
                    Console.WriteLine(index);
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
