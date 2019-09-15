using System;


namespace Sandbox.Mandelbrot
{
    public static class MandelbrotST
    {
        public static int[] Render(int w, int h, double[] area, double[] center)
        {
            int[] pixels = new int[w * h];
            int MAX_ITER = 255;
            double zx, zy, cx, cy, temp;
            double aspect = w / h;
            if (aspect < 1)
            {
                area[1] /= aspect;
            }
            else
            {
                area[0] *= aspect;
            }
            Console.WriteLine(aspect);

            for (int index = 0, y = 0; y < h; y++)
            {
                if (y % 1000 == 0)
                {
                    double progress = Auxiliary.MapDouble(y, 0, h, 0, 100);
                    Console.WriteLine(progress + "% finished rendering.");
                }
                for (int x = 0; x < w; x++)
                {
                    zx = zy = 0;
                    cx = Auxiliary.MapDouble(x, 0, w, -area[0], area[0]);
                    cy = Auxiliary.MapDouble(y, 0, h, -area[1], area[1]);

                    //cx = (x - w / 2.0f) * (4.0f) / w;
                    //cy = (y - h / 2.0f) * (4.0f) / h;

                    int iter = 0;
                    while (zx * zx + zy * zy < 4 && iter++ < MAX_ITER)
                    {
                        temp = zx * zx - zy * zy + cx;
                        zy = 2 * zx * zy + cy;
                        zx = temp;
                    }
                    if (iter < MAX_ITER)
                    {
                        pixels[index] += iter;
                    }
                    index++;
                }
            }
            return pixels;
        }

    }
}
