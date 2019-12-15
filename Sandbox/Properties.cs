using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Properties
    {

        private Color from;
        private Color to;
        private String name;
        private double[,] edges; //North, East, South, West
        private int bailout;
        private int cutoff;
        private int height;
        private int highest;
        private int width;
        private double zoom;
        private double aspectRatio;
        private readonly DateTime initialTime = DateTime.Now;

        public Color From { get => from; set => from = value; }
        public Color To { get => to; set => to = value; }
        public TimeSpan TimeStamp { get => DateTime.Now - initialTime; }
        public double[,] Edges { get => edges; set => edges = value; } //North, East, South, West
        public int Bailout { get => bailout; set => bailout = value; }
        public int Cutoff { get => cutoff; set => cutoff = value; }
        public int Height { get => height; set => height = value; }
        public int Highest { get => highest; set => highest = value; }
        public int Width { get => width; set => width = value; }
        public string Name { get => name; set => name = value; }
        public double Zoom { get => zoom; set => zoom = value; }
        public double AspectRatio { get => aspectRatio; set => aspectRatio = value; }

        public string[] ToStringArray()
        {

            string edges_output = "";
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                edges_output += "[";
                for (int j = 0; j < edges.GetLength(1); j++)
                {
                    edges_output +=string.Format("{0} ", edges[i, j]);
                }
                edges_output += "], ";
            }

            string[] lines = {
                nameof(from)+": "+from.ToString(),
                nameof(to)+": "+ to.ToString(),
                nameof(name)+": "+ name.ToString(),
                nameof(edges)+": "+ edges_output,
                nameof(bailout)+": "+ bailout.ToString(),
                nameof(cutoff)+": "+ cutoff.ToString(),
                nameof(height)+": "+ height.ToString(),
                nameof(highest)+": "+ highest.ToString(),
                nameof(width)+": "+ width.ToString(),
                nameof(zoom)+": "+ zoom.ToString(),
                nameof(initialTime)+": "+ initialTime.ToString()
            };

            return lines;
        }
    }


}
