using System;
using System.Threading;

namespace Sandbox.Mandelbrot
{
    public class MandelbrotMT
    {
        public static int[] Render(int w, int h, double[] magnitude, double[] center)
        {
            int[] exposure = new int[w * h];

            double aspect = w / h;
            if (aspect < 1)
            {
                magnitude[1] /= aspect;
            }
            else
            {
                magnitude[0] *= aspect;
            }

            int processors = Environment.ProcessorCount;
            Thread[] threads = new Thread[processors];

            for (int i = 0; i < processors; i++)
            {
                int hStart = (h / processors) * i;
                int hStop = (h / processors) * (i + 1);
                Console.WriteLine(hStart + ", " + hStop);
                threads[i] = new Thread(() => MyThread(exposure, magnitude, w, h, hStart, hStop))
                {
                    Priority = ThreadPriority.Highest
                };
                threads[i].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            return exposure;
        }

        private static void MyThread(int[] exposure, double[] magnitude, int w, int h, int hStart, int hStop)
        {

            int MAX_ITER = 255;
            double zx, zy, cx, cy, temp;

            for (int y = hStart; y < hStop; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    zx = zy = 0;
                    cx = Auxiliary.MapDouble(x, 0, w, -magnitude[0], magnitude[0]);
                    cy = Auxiliary.MapDouble(y, 0, h, -magnitude[1], magnitude[1]);
                    int index = x + (y * w);
                    int iter = 0;
                    while (zx * zx + zy * zy < 4 && iter++ < MAX_ITER)
                    {
                        temp = zx * zx - zy * zy + cx;
                        zy = 2 * zx * zy + cy;
                        zx = temp;
                    }
                    if (iter < MAX_ITER)
                    {
                        exposure[index] += iter;
                    }
                }
            }

        }
    }
}
