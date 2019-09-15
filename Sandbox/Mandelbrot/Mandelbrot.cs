using System;
using System.Threading.Tasks;

namespace Sandbox.Mandelbrot
{
    public class Mandelbrot
    {

        public static int[] Render(int w, int h, double[] domain)
        {
            int[] pixels = new int[w * h];
            int BAILOUT = 1000;

            Parallel.For(0, w, x =>
            {

                for (int y = 0; y < h; y++)
                {
                    double a = Auxiliary.MapDouble(x, 0, w, domain[0], domain[1]);
                    double b = Auxiliary.MapDouble(y, 0, h, domain[2], domain[3]);
                    
                    //Complex c = new Complex(a, b);
                    double[] c = { a, b };
                    //Complex z = new Complex(0, 0);
                    double[] z = { 0,0 };
                    int iter = 0;
                    do
                    {
                        //z.Square();
                        double temp = (z[0] * z[0]) - (z[1] * z[1]);
                        z[1] = 2.0 * z[0] * z[1];
                        z[0] = temp;

                        //z.Add(c);
                        z[0] += c[0];
                        z[1] += c[1];

                        //if (z.Magnitude() > 2.0) break;
                        if (Math.Sqrt(z[0] * z[0] + z[1] * z[1]) > 2.0) break;
                    } while (iter++ < BAILOUT);
                    pixels[y * w + x] = iter;

                }

            });

            return pixels;
        }
    }
}