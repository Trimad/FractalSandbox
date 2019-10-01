using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Sandbox.Fractal;

namespace Sandbox
{
    public class Program
    {
        static int[] exposure, pixels;
        static void Main(string[] args)
        {

            Properties properties = new Properties
            {
                Bailout = 1000,
                Cutoff = 2,
                Edges = new double[,] { { 0, -2 }, { 2, 0 }, { 0, 2 }, { -2, 0 } },
                From = Color.FromArgb(18, 53, 86),
                Highest = 10000,
                Name = "test",
                To = Color.FromArgb(255, 255, 255),
                Height = 2048,
                Width = 2048,
                Zoom = 1
            };

            UI(properties);

            GCHandle bitsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            Bitmap image = new Bitmap(properties.Width, properties.Height, properties.Width * 4, PixelFormat.Format32bppArgb, bitsHandle.AddrOfPinnedObject());
            Console.WriteLine(properties.TimeStamp + " - Saving "+properties.Name+ "...");
            string text_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Output", properties.Name+".txt");
            string image_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"Output", properties.Name + ".png");

            Console.WriteLine(text_path);
            Console.WriteLine(image_path);
            System.IO.File.WriteAllLines(text_path, properties.ToStringArray());
            image.Save(image_path + ".png", ImageFormat.Png);

        }

        static void UI(Properties p) {
            
            StringBuilder sb = new StringBuilder("! Inputs must be formated as two numbers separated by a space character. !\n");
            sb.Append("Enter the first number that corresponds with the fractal of your choice:\n");
            sb.Append("1. Barnsley Fern\n");
            sb.Append("2. Buddhabrot\n");
            sb.Append("3. Burning Ship\n");
            sb.Append("4. Julia\n");
            sb.Append("5. Mandelbrot\n");

            sb.Append("Enter the second number that corresponds with the coloring algorithm of your choice:\n");
            sb.Append("1. Normalize the exposure to a value between 0 and 255. \n");
            sb.Append("2. Iterations\n");
            sb.Append("3. Lerp\n");
            sb.Append("4. \n");
            sb.Append("5. \n");
            Console.WriteLine(sb.ToString());

            string input = Console.ReadLine();
            string[] split = input.Split(' ', '\t');
            int choose_fractal = Convert.ToInt32(split[0]);
            int choose_colorize = Convert.ToInt32(split[1]);

            switch (choose_fractal)
            {
                case 1:
                    Console.WriteLine(p.TimeStamp + " - Began rendering Barnsley Fern.");
                    exposure = BarnsleyFern.Render(p);
                    Console.WriteLine(p.TimeStamp + " - Completed rendering Barnsley Fern.");
                    break;
                case 2:
                    Console.WriteLine(p.TimeStamp + " - Began rendering Buddhabrot.");
                    exposure = Buddhabrot.Render(p);
                    Console.WriteLine(p.TimeStamp + " - Completed rendering Budhabrot.");
                    break;
                case 3:
                    Console.WriteLine(p.TimeStamp + " - Began rendering Burning Ship.");
                    exposure = BurningShip.Render(p);
                    Console.WriteLine(p.TimeStamp + " - Completed rendering Burning Ship.");
                    break;
                case 4:
                    Console.WriteLine(p.TimeStamp + " - Began rendering Juila Set.");
                    exposure = Julia.Render(p);
                    Console.WriteLine(p.TimeStamp + " - Completed rendering Julia Set.");
                    break;
                case 5:
                    Console.WriteLine(p.TimeStamp + " - Began endering Mandelbrot Set.");
                    exposure = Mandelbrot.Render(p);
                    Console.WriteLine(p.TimeStamp + " - Completed rendering Mandelbrot Set.");
                    break;
                default:
                    exposure = null;
                    break;
            }

            switch (choose_colorize)
            {
                case 1:
                    Console.WriteLine(p.TimeStamp + " - Began Normalize");
                    pixels = Colorize.Normalize(exposure, p);
                    Console.WriteLine(p.TimeStamp + " - Completed Normalize.");
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }
                    
        }
    }

}