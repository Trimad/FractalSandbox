using Sandbox.Fractals;
using System;
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
        static int[] pixels;
        static double[,,] domain;
        static Properties properties = new Properties
        {
            Bailout = 2000,
            Cutoff = 3,
            Edges = new double[,] { { 0, -2 }, { 2, 0 }, { 0, 2 }, { -2, 0 } }, //NESW from (0,0)
            From = Color.FromArgb(0, 0, 0),
            Highest = 100000,
            To = Color.FromArgb(255, 255, 255),
            Width = 1920*4,
            Height = 1080*4,
            //Zoom = 1,
            AspectRatio = 1.77777777778
        };

        static void Main(string[] args)
        {
            Console.WriteLine(properties.ToString());
            pixels = new int[properties.Width*properties.Height];
            domain = new double[properties.Width, properties.Height, 2];

            for (int x = 0; x < properties.Width; x++)
            {
                double mx = Auxiliary.MapDouble(x, 0, properties.Width, -2 * properties.AspectRatio, 2 * properties.AspectRatio);
                for (int y = 0; y < properties.Height; y++)
                {
                    double my = Auxiliary.MapDouble(y, 0, properties.Height, -2, 2);
                    domain[x, y, 0] = mx;
                    domain[x, y, 1] = my;
                }

            }

            UI();

            GCHandle bitsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            Bitmap image = new Bitmap(properties.Width, properties.Height, properties.Width * 4, PixelFormat.Format32bppArgb, bitsHandle.AddrOfPinnedObject());
            Console.WriteLine(properties.TimeStamp + " - Saving "+properties.Name+ "...");
            string text_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Output", properties.Name+".txt");
            string image_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Output", properties.Name + ".png");

            //Console.WriteLine(text_path);
            //Console.WriteLine(image_path);
            File.WriteAllLines(text_path, properties.ToStringArray());
            image.Save(image_path, ImageFormat.Png);

        }

        static void UI() {

            List<Fractal> fractals = new List<Fractal>
            {
                //new BarnsleyFern(),
                //new BarnsleyFernDistance(),
                new Buddhabrot(pixels, domain, properties),
                //new BuddhabrotDistance(),
                //new BurningShip(),
                new Julia(pixels, domain, properties)
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
            string input = "0 3";
            string[] split = input.Split(' ', '\t');
            int choose_fractal = Convert.ToInt32(split[0]);
            int choose_colorize = Convert.ToInt32(split[1]);

            properties.Name = fractals[choose_fractal].name;
            //Console.WriteLine(properties.TimeStamp + " - Began rendering " + properties.Name);
            //pixels = fractals[choose_fractal].Render(properties);
            //Auxiliary.Save(pixels, properties.Name);
            pixels = Auxiliary.Load(properties.Name);
            Console.WriteLine(properties.TimeStamp + " - Completed rendering " + properties.Name);

            switch (choose_colorize)
            {
                case 0:
                    Console.WriteLine(properties.TimeStamp + " - Began Iterations.");
                    pixels = Stylize.Iterations(pixels, properties);
                    Console.WriteLine(properties.TimeStamp + " - Completed Iterations.");
                    break;
                case 1:
                    Console.WriteLine(properties.TimeStamp + " - Began Lerp.");
                    pixels = Stylize.Lerp(pixels, properties);
                    Console.WriteLine(properties.TimeStamp + " - Completed Lerp.");
                    break;
                case 2:
                    Console.WriteLine(properties.TimeStamp + " - Began Log10Color.");
                    pixels = Stylize.Log10Color(pixels, properties);
                    Console.WriteLine(properties.TimeStamp + " - Completed Log10Color.");
                    break;
                case 3:
                    Console.WriteLine(properties.TimeStamp + " - Began Ramp.");
                    pixels = Stylize.Ramp(pixels, properties, 4);
                    Console.WriteLine(properties.TimeStamp + " - Completed Ramp.");
                    break;
                case 4:
                    Console.WriteLine(properties.TimeStamp + " - Began Sine.");
                    pixels = Stylize.Sine(pixels, properties);
                    Console.WriteLine(properties.TimeStamp + " - Completed sine.");
                    break;
                //case 5:
                //    break;
                default:
                    break;
            }
                    
        }
    }

}