using System;
using System.Threading.Tasks;

namespace Sandbox.Fractal
{
    public class Mandelbrot { 

            public static int[] Render(Properties p)
            {
            int[] pixels = new int[p.Width * p.Height];

            Parallel.For(0, p.Width, x =>
                {
                    //Complex zLast = new Complex(0, 0);

                    for (int y = 0; y < p.Height; y++)
                    {

                        double a = 2 * (x - p.Width * 0.5) / (0.5 * p.Zoom * p.Width);
                        double b = 2 * (y - p.Height * 0.5) / (0.5 * p.Zoom * p.Height);

                        Complex c = new Complex(a, b);
                        Complex z = new Complex(0, 0);

                        int iter = 0;

                        do
                        {
                            z.Square();
                            z.Add(c);

                            if (z.Magnitude() > 2) break;
                        } while (iter++ < p.Bailout);

                    }

                });

                return pixels;
            }
        }
    }
