using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Fractal_Generator
{
    static class Program
    {

        static Settings settings = new Settings
        {
            Bailout = 1500,
            Cutoff = 0,
            Edges = new double[,] { { 0, -2 }, { 2, 0 }, { 0, 2 }, { -2, 0 } }, //NESW from (0,0)
            From = Color.FromArgb(0, 0, 0),
            Highest = 10000,
            To = Color.FromArgb(255, 255, 255),
            Width = 1920,
            Height = 1080,
            AspectRatio = 1.77777777778,
            FractalIndex=0,
            ShaderIndex=0
        };

        static List<Fractal> fractals = new List<Fractal>
            {
                new Buddhabrot(pixels, domain, settings)
            };

        static readonly List<Shader> shaders = new List<Shader>
            {
                new Shader("Linear Interpolation"),
                new Shader("Log-Base-10"),
                new Shader("Ramp Brightness"),
                new Shader("Raw Iterations"),
                new Shader("Sine Wave"),
            };

        static int[] pixels;
        static double[,,] domain;

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyForm(settings, fractals, shaders));

            pixels = new int[settings.Width * settings.Height];
            domain = new double[settings.Width, settings.Height, 2];

            for (int x = 0; x < settings.Width; x++)
            {
                double mx = Auxiliary.MapDouble(x, 0, settings.Width, -2 * settings.AspectRatio, 2 * settings.AspectRatio);
                for (int y = 0; y < settings.Height; y++)
                {
                    double my = Auxiliary.MapDouble(y, 0, settings.Height, -2, 2);
                    domain[x, y, 0] = mx;
                    domain[x, y, 1] = my;
                }

            }
 


        }
    }
}
