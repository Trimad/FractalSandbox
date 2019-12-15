using Sandbox.Fractals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Sandbox
{
    public class Program
    {
        static int[] exposure;
        static int[] pixels;
        static double[,,] domain;
        static void Main(string[] args)
        {

            Properties properties = new Properties
            {
                Bailout = 500,
                Cutoff = 1,
                Edges = new double[,] { { 0, -2 }, { 2, 0 }, { 0, 2 }, { -2, 0 } }, //NESW from (0,0)
                From = Color.FromArgb(127, 0, 0),
                Highest = 10000,
                To = Color.FromArgb(127, 255, 255),
                Width = 1920,
                Height = 1080,
                Zoom = 1,
                AspectRatio = 1.77777777778
            };

            pixels = new int[properties.Width * properties.Height];
            domain = new double[properties.Width, properties.Height, 2];

            for (int x = 0; x < properties.Width; x++)
            {
                double mx = Auxiliary.MapDouble(x, 0, properties.Width, -2, 2);

                for (int y = 0; y < properties.Height; y++)
                {
                    double my = Auxiliary.MapDouble(y, 0, properties.Height, -2, 2);
                    domain[x, y, 0] = mx;
                    domain[x, y, 1] = my;
                }

            }

            UI(properties);

            GCHandle bitsHandle = GCHandle.Alloc(exposure, GCHandleType.Pinned);
            Bitmap image = new Bitmap(properties.Width, properties.Height, properties.Width * 4, PixelFormat.Format32bppArgb, bitsHandle.AddrOfPinnedObject());
            Console.WriteLine(properties.TimeStamp + " - Saving "+properties.Name+ "...");
            string text_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Output", properties.Name+".txt");
            string image_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Output", properties.Name + ".png");

            Console.WriteLine(text_path);
            Console.WriteLine(image_path);
            File.WriteAllLines(text_path, properties.ToStringArray());
            image.Save(image_path, ImageFormat.Png);

        }

        static void UI(Properties p) {

            List<Fractal> fractals = new List<Fractal>
            {
                //new BarnsleyFern(),
                //new BarnsleyFernDistance(),
                new Buddhabrot(pixels,domain),
                //new BuddhabrotDistance(),
                //new BurningShip(),
                //new Julia(),
                //new Mandelbrot(),
                //new MandelbrotDistance(),
                //new SerpinskiTriangle()
            };

            StringBuilder sb = new StringBuilder();
            sb.Append("Enter the first number that corresponds with the fractal of your choice:\n");
            for (int i = 0; i < fractals.Count; i++) {
                sb.Append("\t" + i + ". " + fractals[i].name + "\n");
            }
            sb.Append("\n");
            sb.Append("Enter the second number that corresponds with the coloring algorithm of your choice:\n");
            sb.Append("\t0. Draw the raw iterations as a monochrome brightness value.\n");
            sb.Append("\t1. Linear interpolation between two color values.\n");
            sb.Append("\t2. Interpolate between two colors using log-base-ten.\n");
            Console.WriteLine(sb.ToString());

            //string input = Console.ReadLine();
            string input = "0 2";
            string[] split = input.Split(' ', '\t');
            int choose_fractal = Convert.ToInt32(split[0]);
            int choose_colorize = Convert.ToInt32(split[1]);

            p.Name = fractals[choose_fractal].name;
            Console.WriteLine(p.TimeStamp + " - Began rendering " + p.Name);
            exposure = fractals[choose_fractal].Render(p);
            Console.WriteLine(p.TimeStamp + " - Completed rendering " + p.Name);

            switch (choose_colorize)
            {
                case 0:
                    Console.WriteLine(p.TimeStamp + " - Began Iterations.");
                    exposure = Stylize.Iterations(exposure, p);
                    Console.WriteLine(p.TimeStamp + " - Completed Iterations.");
                    break;
                case 1:
                    Console.WriteLine(p.TimeStamp + " - Began Lerp.");
                    exposure = Stylize.Lerp(exposure, p);
                    Console.WriteLine(p.TimeStamp + " - Completed Lerp.");
                    break;
                case 2:
                    Console.WriteLine(p.TimeStamp + " - Began Log10Color.");
                    exposure = Stylize.Log10Color(exposure, p);
                    Console.WriteLine(p.TimeStamp + " - Completed Log10Color.");
                    break;
                //case 3:
                    //Console.WriteLine(p.TimeStamp + " - Began Normalize.");
                    //Auxiliary.NormalizeArray(exposure_iter);
                    //Console.WriteLine(p.TimeStamp + " - Completed Normalize.");
                    //break;
                //case 4:
                    //exposure_data = Auxiliary.NormalizeArray(exposure_data);
                //    break;
                //case 5:
                //    break;
                default:
                    break;
            }
                    
        }
    }

}