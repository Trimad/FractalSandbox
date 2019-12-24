using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sandbox.Fractals
{
    public class Julia:Fractal
    {
        public Julia(int[] pix, double[,,] dom, Properties pro) {
            name = "Julia";
            pixels = pix;
            domain = dom;
            properties = pro;
        }

        public override int[] Render(Properties p)
        {


            Complex c = new Complex(-0.7, 0.27015);

            for (int x = 0; x < properties.Width; x++)
            {
                for (int y = 0; y < properties.Height; y++)
                {
                    int iteration = 0;
                    double zx = Auxiliary.MapDouble(x, 0, p.Width, domain[0, 0, 0], domain[properties.Width - 1, 0, 0]);
                    double zy = Auxiliary.MapDouble(y, 0, p.Height, domain[0, 0, 1], domain[0, properties.Height - 1, 1]);
                    Complex z = new Complex(zx, zy);
                    do
                    {
                        z.Square();
                        z.Add(c);
                    }
                    while (z.Magnitude() <= 2.0 && iteration++ < properties.Bailout);

                    pixels[x + y * properties.Width] = iteration;
                }
            }
            return pixels;

        }
    }
}