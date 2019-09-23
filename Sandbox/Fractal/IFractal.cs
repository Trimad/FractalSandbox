using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Fractal
{
    public interface IFractal
    {
        int[] Render(Properties p);
        void Iterate();

    }
}
