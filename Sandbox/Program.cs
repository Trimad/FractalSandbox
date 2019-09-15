using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Sandbox.Fractal;

namespace Sandbox
{

    public class Program
    {
        static void Main(string[] args)
        {

            Properties properties = new Properties
            {
                Bailout = 3000,
                Cutoff = 2,
                Edges = new double[,] { { 0, -2 }, { 2, 0 }, { 0, 2 }, { -2, 0 } },
                //From = Color.FromArgb(18, 53, 86),
                From = Color.FromArgb(86, 18, 18),
                Highest = 100000,
                Name = "red barnsley fern",
                To = Color.FromArgb(255, 255, 255),
                Height = 1080<<2,
                Width = 1920<<2,
            };
            
            //double aspect = (double)WIDTH / (double)HEIGHT;
            ////Console.WriteLine(aspect);

            //if (aspect <= 1)
            //{
            //    DOMAIN[2] /= aspect;
            //    DOMAIN[3] /= aspect;
            //}
            //else
            //{
            //    DOMAIN[0] *= aspect;
            //    DOMAIN[1] *= aspect;
            //    Console.WriteLine("DOMAIN[0]: "+DOMAIN[0].ToString());
            //    Console.WriteLine("DOMAIN[1]: " + DOMAIN[1].ToString());
            //    DOMAIN[0] += (double)(DOMAIN[1]/2.0);
            //    DOMAIN[1] += (double)(DOMAIN[1]/2.0);
            //    Console.WriteLine("DOMAIN[0]: " + DOMAIN[0].ToString());
            //    Console.WriteLine("DOMAIN[1]: " + DOMAIN[1].ToString());
            //}
            
            int[] exposure = BarnsleyFern.Render(properties);
            int[] pixels = Colorize.Log10Color(exposure, properties);

            GCHandle bitsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            Bitmap image = new Bitmap(properties.Width, properties.Height, properties.Width * 4, PixelFormat.Format32bppArgb, bitsHandle.AddrOfPinnedObject());
            Console.WriteLine(properties.TimeStamp + " - Saving "+properties.Name+ "...");
            System.IO.File.WriteAllLines("C:\\Users\\matte\\source\\repos\\Sandbox\\Sandbox\\output\\" + properties.Name + ".txt", properties.ToStringArray());
            image.Save("C:\\Users\\matte\\source\\repos\\Sandbox\\Sandbox\\output\\" + properties.Name + ".png", ImageFormat.Png);

        }

    }

}