﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Auxiliary
    {

        public static double RandomGaussian(double mean, int stdDev) {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

        public static double Random() {
            Random rand = new Random();
            return rand.NextDouble()-0.5;
        }

        public static void Save(int[] pixels, string file) {

            byte[] myFinalBytes = new byte[pixels.Length * 4];
            for (int cnt = 0; cnt < pixels.Length; cnt++)
            {
                byte[] myBytes = BitConverter.GetBytes(pixels[cnt]);
                Array.Copy(myBytes, 0, myFinalBytes, cnt * 4, 4);
            }

            File.WriteAllBytes(file, myFinalBytes);
        }
        public static int[] Load(string file)
        {
            byte[] inputElements = File.ReadAllBytes(file);

            int[] myFinalIntegerArray = new int[inputElements.Length / 4];
            for (int cnt = 0; cnt < inputElements.Length; cnt += 4)
            {
                myFinalIntegerArray[cnt / 4] = BitConverter.ToInt32(inputElements, cnt);
            }

            return myFinalIntegerArray;
        }

        public static float MapFloat(float value, float istart, float istop, float ostart, float ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }

        public static double MapDouble(double value, double istart, double istop, double ostart, double ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }
        public static double CustomLog(double n, double b)
        {
            return (Math.Log(n) / Math.Log(b));
        }

        public static double Lerp(double first, double second, double by)
        {
            return first + (second - first) * by;
        }

        public static double Slope(Point p1, Point p2) {
            double m = ((double)p2.Y- (double)p1.Y)/((double)p2.X+ (double)p1.X);
            return m;
        }

        public static double Normalize(int value, int min, int max)
        {
            //if (min == 0 || max == 0)
            //{
            //    return value;
            //}
            return (value - min) / (max - min);
        }
        public static double Normalize(double value, double min, double max)
        {
            //if (min == 0 || max == 0)
            //{
            //    return value;
            //}
            return (value - min) / (max - min);
        }

        //Returns an array of values between 0 and 1.
        //public static double[] NormalizeArray(int[] exposure, double[] normalized)
        //{

        //    int min = int.MaxValue;
        //    int max = 0;

        //    Parallel.For(0, exposure.Length, i =>
        //    {
        //        if (exposure[i] < min) { min = exposure[i]; }
        //        if (exposure[i] > max) { max = exposure[i]; }
        //    });

        //    _ = Parallel.For(0, exposure.Length, i =>
        //    {
        //        double norm = Auxiliary.Normalize(exposure[i], min, max);
        //        normalized[i] = norm;

        //    });
        //    return normalized;
        //}
        //INPUT: An array of doubles
        //OUTPUT: An array of doubles normalized between 0 and 1
        public static double[] NormalizeArray(double[] arr)
        {
            double[] norm_arr = new double[arr.Length];
            double min = 99999;
            double max = 0;

            Parallel.For(0, arr.Length, i =>
            {
                if (arr[i] < min) { min = arr[i]; Console.WriteLine("min: " + min); }
                if (arr[i] > max) { max = arr[i]; Console.WriteLine("max: " + max); }
            });
            _ = Parallel.For(0, norm_arr.Length, i =>
            {
                double norm = Auxiliary.Normalize(arr[i], min, max);
                norm_arr[i] = norm;

            });
            return norm_arr;
        }

        //INPUT: An array of integers 
        //OUTPUT: An array of doubles normalized between 0 and 1
        public static double[] NormalizeArray(int[] exposure)
        {
            double[] norm_arr = new double[exposure.Length];
            int min = int.MaxValue;
            int max = 0;
            //1. Find the minimum and maximum values.
            Parallel.For(0, exposure.Length, i =>
            {
                if (exposure[i] < min) { min = exposure[i]; }
                if (exposure[i] > max) { max = exposure[i]; }
            });
            //2. Normalize every pixel to a value between 0 and 1.
            _ = Parallel.For(0, exposure.Length, i =>
            {
                double norm = Auxiliary.Normalize((double)exposure[i], (double)min, (double)max);
                norm_arr[i] = norm;
            });
            return norm_arr;
        }

        public static double Distance(double x1, double y1, double x2, double y2) {
            double p1 = x2 - x1;
            double p2 = y2 - y1;
            double hypotenuse = Math.Sqrt((p1 * p1) + (p2 * p2));
            return hypotenuse;
        }
    }
}
