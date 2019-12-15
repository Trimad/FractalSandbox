using System;
using System.Threading.Tasks;

namespace Sandbox.Fractals
{
    public class MandelbrotDistance:Fractal {
        public MandelbrotDistance() {
            name = "Mandelbrot, distance-based";
        }

        //public override double[] Render(Properties p)
        //    {
        //    double[] distance_arr = new double[p.Width * p.Height];

        //    //Parallel.For(0, p.Width, x =>
        //    for (int x = 0; x < p.Width; x++)
        //    {

        //        for (int y = 0; y < p.Height; y++)
        //        {
        //            //double a = Auxiliary.MapDouble(x, 0, p.Width, -2, 2);
        //            //double b = Auxiliary.MapDouble(y, 0, p.Height, -2, 2);

        //            double a = 2 * (double)(x - p.Width * 0.5) / (double)(0.5 * p.Zoom * p.Width);
        //            double b = 2 * (double)(y - p.Height * 0.5) / (double)(0.5 * p.Zoom * p.Height);

        //            Complex c = new Complex(a, b);
        //            Complex z = new Complex(0, 0);

        //            int iter = 0;

        //            do
        //            {
        //                z.Square();
        //                z.Add(c);

        //                if (z.Magnitude() > 2) break;
        //            } while (iter++ < p.Bailout);

        //            double distance = z.Distance(c);
        //            distance_arr[y * p.Width + x] = distance;

        //        }

        //    }
        //    //);

        //        return distance_arr;
        //    }
        }
    }
