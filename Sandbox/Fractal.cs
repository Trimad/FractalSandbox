﻿namespace Sandbox
{
    public class Fractal
    {
        internal string name;
        internal int[] pixels;
        internal double[,,] domain;
        internal Properties properties;
        public virtual int[] Render(Properties p) {
            int[] pixels = new int[0];
            return pixels;
        }
    }
}
