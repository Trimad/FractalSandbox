using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Complex
    {
        public double a; //real
        public double b; //imaginary

        public Complex(double a, double b) {
            this.a = a;
            this.b = b;
        }

        public void Square() {
            double temp = (a * a) - (b * b);
            b = 2.0 * a * b;
            a = temp;
        }

        public double Magnitude() {
         
            return Math.Sqrt(a * a + b * b);
        }

        public void SquareRotated()
        {
            double temp = (a * a) - (b * b);
            a = 2.0 * a * b;
            b = temp;
        }

        public double Distance(Complex c) {
            double p1 = c.a - a;
            double p2 = c.b - b;
            double hypotenuse = Math.Sqrt((p1 * p1) + (p2 * p2));
            //Console.WriteLine(hypotenuse);
            return hypotenuse;
            
        }
        public void Add(Complex c) {
            a += c.a;
            b += c.b;
        }
        public void AddRotated(Complex c)
        {
            a += c.b;
            b += c.a;
        }
    }
}
