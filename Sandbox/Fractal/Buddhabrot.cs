using System;
using System.Threading.Tasks;

namespace Sandbox.Fractal
{
    public class Buddhabrot
    {
        private static int highest = 0;
        private static int zoom = 1;
        public static int[] Render(Properties p)
        {
            int[] pixels = new int[p.Width*p.Height];
            while (highest < p.Highest)
            {
                Parallel.For(0, p.Width, x =>
                {
                    for (int y = 0; y < p.Height; y++)
                    {
                        if (Iterate(x, y, p, false, pixels))
                        {
                            Iterate(x, y, p, true, pixels);
                        }
                    }
                });
            }
            highest = 0;
            return pixels;
        }

        //Iterate the Mandelbrot and return TRUE if the point escapes
        //Also handle the drawing of the exit points
        static private bool Iterate(int x, int y, Properties p, bool drawIt, int[] pixels)
        {
            double aspectRatio = (double)p.Width / (double)p.Height;
            double scaledX1 = (double)-2.0 * aspectRatio;
            double scaledX2 = (double)2.0 * aspectRatio;
            //double x0 = Auxiliary.MapDouble(x, 0, p.Width, scaledX1, scaledX2);
            //double y0 = Auxiliary.MapDouble(y, 0, p.Height, -2, 2);

            double x0 = 2 * (x - p.Width * 0.5) / (0.5 * zoom*p.Width);
            double y0 = 2 * (y - p.Height * 0.5) / (0.5 * zoom* p.Height);

            Complex z = new Complex(0, 0);
            Complex zNew;
            Complex c = new Complex(x0, y0);

            //newY = (z[0] * z[0] - z[1] * z[1]) + c[0]; //rotated
            //newX = (2 * z[0] * z[1]) + c[1]; //rotated
            //z[0] = newY; //rotated
            //z[1] = newX; //rotated

            for (int i = 0; i < p.Bailout; i++)
            {
                
                z.Square();
                z.Add(c);
                zNew = new Complex(z.a,z.b);

                if (drawIt && i >= p.Cutoff)
                {


                    int ix = Math.Clamp((int)Auxiliary.MapDouble(zNew.a, scaledX1, scaledX2, 0, p.Width-1),0,p.Width-1);
                    //ix += +rand.Next(-1, 1);
                    int iy = Math.Clamp((int)Auxiliary.MapDouble(zNew.b, -2, 2, 0, p.Height-1), 0, p.Height-1);
                    //iy += +rand.Next(-1, 1);
                    int index = iy * p.Width + ix;
                    pixels[index]++;
                    if (highest < pixels[index]) { highest = pixels[index]; }
                    
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