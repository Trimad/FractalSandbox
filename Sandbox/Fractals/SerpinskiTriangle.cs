using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Fractals
{
    class SerpinskiTriangle : Fractal
    {
        public SerpinskiTriangle() {
            name = "Serpinski Triangle";
        }

        public override int[] Render(Properties p) {
            int[] pixels = new int[p.Width*p.Height];
            Random rand = new Random();
            Parallel.For(0, 1000, x =>
            { 
            
            });
                return pixels;
        }

    }
}
