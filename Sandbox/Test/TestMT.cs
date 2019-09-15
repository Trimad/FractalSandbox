using System;
using System.Threading;

namespace Sandbox.Test
{
    public static class TestMT
    {

        public static int[] Render(int w, int h, double[] area, double[] center)
        {
            
            int[] exposure = new int[w * h];

            int processors = Environment.ProcessorCount;
            Thread[] threads = new Thread[processors];

            for (int i = 0; i < processors; i++)
            {
                int start = (w / processors) * i;
                int stop = (w / processors) * (i + 1);
                threads[i] = new Thread(() => MyThread(exposure, w, start, stop, h))
                {
                    Priority = ThreadPriority.Highest
                };
                threads[i].Start();
            }


            return exposure;
        }

        private static void MyThread(int[] exposure, int w, int wStart, int wStop, int h)
        {

            Random random = new Random();
            for (int y = 0; y < h; y++)

            {

                for (int x = wStart; x < wStop; x++)
                {
                    int index = x + (y * w);
                    exposure[index] = random.Next(0, 255);
                }

            }

        }
    }
}
