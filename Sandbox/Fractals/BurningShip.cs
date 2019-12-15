using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//public static double[] DOMAIN = { -2.5, 1.0, -1.0, 1.0 }; //Burning Ship
//public static double[] DOMAIN = { -2.5, 1.0, -1.0, 1.0 }; //Burning Ship
//public static double[] DOMAIN = { -2.5, 1.0, -1.0, 1.0 }; //Burning Ship

namespace Sandbox.Fractals
{
    public class BurningShip:Fractal
    {
        public BurningShip() {
            name = "Burning Ship";
        }

        public override int[] Render(Properties p)
        {
            int[] pixels = new int[p.Width * p.Height];

            Parallel.For(0, p.Width, x =>
            {

                for (int y = 0; y < p.Height; y++)
                {
                    double a = Auxiliary.MapDouble(x, 0, p.Width, -2.5, 1.0);
                    double b = Auxiliary.MapDouble(y, 0, p.Height, -1.0, 1.0);

                    //Complex c = new Complex(a, b);
                    //Complex z = new Complex(0, 0);
                    double[] c = { a, b };
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
                    } while (iter++ < p.Bailout);

                    pixels[y * p.Width + x] = iter;

                }

            });

            return pixels;
        }
    }
}
