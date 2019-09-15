using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Mandelbrot
{
    public class MandelbrotTest { 

            public static int[] Render(int w, int h, double[] domain)
            {
                int[] pixels = new int[w * h];
                int BAILOUT = 1000;

                Parallel.For(0, w, x =>
                {
                    //Complex zLast = new Complex(0, 0);

                    for (int y = 0; y < h; y++)
                    {
                        double a = Auxiliary.MapDouble(x, 0, w, domain[0], domain[1]);
                        double b = Auxiliary.MapDouble(y, 0, h, domain[2], domain[3]);

                        Complex c = new Complex(a, b);
                        Complex z = new Complex(0, 0);

                        int iter = 0;
                        double r = 20; //escape radius
                        double r2 = r*r; 
                        do
                        {
                            z.Square();
                            z.Add(c);

                            if (z.Magnitude() > r) break;
                        } while (iter++ < BAILOUT);


                        if (iter < BAILOUT)
                        {
                            double dist = z.Magnitude(); //distance from origin
                            double fracIter = (dist - r) / (r2 - r); //linear interpolation
                            //fracIter = Math.Log(dist) / Math.Log(r) - 1;
                            int red = (int)(Math.Sin((double)fracIter*0.33)*255);
                            int green = (int)(Math.Sin((double)fracIter *0.66)*255);
                            int blue = (int)(Math.Sin((double)fracIter*0.99)*255);
                            //Console.WriteLine(blue);
                            pixels[y * w + x] = (255 << 24) | (red << 16) | (green << 8) | (blue << 0);
                        }
                        else { pixels[y * w + x] = 255 << 24 | 0 << 16 | 0 << 8 | 0 << 0; }
                    }

                });

                return pixels;
            }
        }
    }
