namespace Fractal_Generator
{
    public class Fractal
    {
        internal string name;
        internal int[] pixels;
        internal double[,,] domain;
        internal Settings properties;
        public virtual int[] Render(Settings p) {
            int[] pixels = new int[0];
            return pixels;
        }
    }
}
