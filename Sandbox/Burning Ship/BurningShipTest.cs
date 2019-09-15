using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//public static double[] DOMAIN = { -2.5, 1.0, -1.0, 1.0 }; //Burning Ship
//public static double[] DOMAIN = { -2.5, 1.0, -1.0, 1.0 }; //Burning Ship
//public static double[] DOMAIN = { -2.5, 1.0, -1.0, 1.0 }; //Burning Ship

namespace Sandbox.BurningShip
{
    public static class BurningShipTest
    {
        public static int[] Render(int w, int h, double[] domain)
        {
            int[] pixels = new int[w * h];
            int BAILOUT = 1000;

            var Options = new ParallelOptions();

            // Keep one core/CPU free...
            Options.MaxDegreeOfParallelism = Environment.ProcessorCount - 1;

            Parallel.For(0, w, x =>
            {

                for (int y = 0; y < h; y++)
                {
                    double a = Auxiliary.MapDouble(x, 0, w, domain[0], domain[1]);
                    double b = Auxiliary.MapDouble(y, 0, h, domain[2], domain[3]);

                    //Complex c = new Complex(a, b);
                    double[] c = { a, b };
                    //Complex z = new Complex(0, 0);
                    double[] z = { 0, 0 };
                    int iter = 0;
                    do
                    {
                        //z.Square();
                        double temp = (z[0] * z[0]) - (z[1] * z[1]);
                        z[1] = Math.Abs(2.0 * z[0] * z[1]);
                        z[0] = Math.Abs(temp);
                        
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
