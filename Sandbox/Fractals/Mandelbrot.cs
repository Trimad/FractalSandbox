using System.Threading.Tasks;

namespace Sandbox.Fractals
{
    public class Mandelbrot: Fractal{

        public Mandelbrot() {
            name = "Mandelbrot";
        
        }

        public override int[] Render(Properties p)
            {
            int[] pixels = new int[p.Width * p.Height];
            Parallel.For(0, p.Width, x =>
                {
                    for (int y = 0; y < p.Height; y++)
                    {
                        double a = Auxiliary.MapDouble(x, 0, p.Width, -2, 2);
                        double b = Auxiliary.MapDouble(y, 0, p.Height, -2, 2);
                        //double a = 2 * (double)(x - p.Width * 0.5) / (double)(0.5 * p.Zoom * p.Width);
                        //double b = 2 * (double)(y - p.Height * 0.5) / (double)(0.5 * p.Zoom * p.Height);
                        Complex c = new Complex(a, b);
                        Complex z = new Complex(0, 0);
                        int iterations = 0;
                        do
                        {
                            z.Square();
                            z.Add(c);
                        } while (z.Magnitude() < 2 && iterations++ < p.Bailout);
                        pixels[x + y * p.Width] = iterations;
                    }
                });
                return pixels;
            }
        }
    }