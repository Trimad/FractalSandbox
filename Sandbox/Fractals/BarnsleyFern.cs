using System;

namespace Sandbox.Fractals
{

    public class BarnsleyFern : Fractal
    {

        public BarnsleyFern() {
            name = "Barnsley Fern";
        }

        //public static readonly double[] DOMAIN = { -2.1820, 2.6558, 0, 9.9983 };
        //Edges = new double[,] { { 0, 9.9983 }, { 2.6558, 0 }, { 0, 0 }, { -2.1820, 0 } } //Barnsley Fern
        private static readonly Random rand = new Random(); //reuse this if you are generating many
        private static readonly double[,] magicX = {
              {0.00, 0.00},
              {0.85, 0.04},
              {0.20, -0.26},
              {-0.15, 0.28}
                };
        private static readonly double[,] magicY = {
              {0.16, 0.00, 0.00},
              {-0.04, 0.85, 1.6},
              {0.23, 0.22, 1.6},
              {0.26, 0.24, 0.44}
                };
        private static double x = 0;
        private static double y = 0;
        private static int highest = 0;
        public override int[] Render(Properties p)
        {            
            int[] exposure = new int[p.Width*p.Height];
            int bound = (p.Width - p.Height) / 2;
            //This first pass draws the Barnsley Fern like normal.
            while (highest < p.Highest)
            {
                getPoint();
                int px = (int)Auxiliary.MapDouble(x, -2.1820, 2.6558, bound, p.Width - bound);
                int py = (int)Auxiliary.MapDouble(y, 0, 9.9983, p.Height, 0);
                int index = px + py * p.Width;
                if (index >= 0 && index < (p.Width * p.Height))
                {
                    exposure[index]++;
                    if (highest < exposure[index]) { highest = exposure[index]; }
                }
            }

            return exposure;
        }

        private unsafe static void getPoint()
        {
            double r1 = rand.NextDouble();
            double dx;
            double dy;
            if (r1 < 0.01)
            {
                //1
                dx = magicX[0, 0];
                dy = magicY[0, 0] * y;
            }
            else if (r1 < 0.86)
            {
                //2
                dx = magicX[1, 0] * x + magicX[1, 1] * y;
                dy = magicY[1, 0] * x + magicY[1, 1] * y + magicY[1, 2];
            }
            else if (r1 < 0.93)
            {
                //3
                dx = magicX[2, 0] * x + magicX[2, 1] * y;
                dy = magicY[2, 0] * x + magicY[2, 1] * y + magicY[2, 2];
            }
            else
            {
                //4
                dx = magicX[3, 0] * x + magicX[3, 1] * y;
                dy = magicY[3, 0] * x + magicY[3, 1] * y + magicY[3, 2];
            }

            x = dx;
            y = dy;
        }

    }
}