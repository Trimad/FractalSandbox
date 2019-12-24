using System;

namespace Fractal_Generator
{
    public class Shader
    {
        internal string name;

       public Shader(string name, Func<int[]> func) {
            this.name = name;
        }

        Func<int[]> f;

        public int[] Draw(int[] arr)
        {
            return f(arr);
        }

    }
}
