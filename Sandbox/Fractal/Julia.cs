using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sandbox.Fractal
{
    public class Julia
    {
        public static int[] Render(Properties p)
        {
            int[] exposure = new int[p.Width * p.Height];
            double zoom = 1;

            Complex c = new Complex(-0.7, 0.27015);

            for (int x = 0; x < p.Width; x++)
            {
                for (int y = 0; y < p.Height; y++)
                {
                    int iteration = 0;
                    //double zx = Auxiliary.MapDouble(x, 0, p.Width, -2, 2);
                    //double zy = Auxiliary.MapDouble(y, 0, p.Height, -2, 2);
                    double zx = 1.5 * (x - p.Width *0.5) / (0.5 * zoom * p.Width);
                    double zy = 1.0 * (y - p.Height * 0.5) / (0.5 * zoom * p.Height);
                    Complex z = new Complex(zx, zy);
                    do
                    {
                        z.Square();
                        z.Add(c);
                    }
                    while (z.Magnitude() <= 2.0 && iteration++ < p.Bailout);
                    exposure[x + y * p.Width] = iteration;

                }


            }
            return exposure;

        }
    }
}