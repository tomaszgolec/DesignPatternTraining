using System;

namespace FactoryMethod
{

    public class Point
    {
        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        private double x, y;
        /// <summary>
        /// Because we don't know which parameter is x or y o theta or beta we must introduce explanation here :( 
        /// </summary>
        /// <param name="a">x if cartesian, rho if polar </param>
        /// <param name="b"></param>
        /// <param name="system"></param>
        public Point(double a, double b, CoordinateSystem system =CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }

        //because we don't have factory we cannot prepare different type of creating object
        //like below for example
        //public Point(double beta, double theta)
        //{
        //    different type of creation 
        //}

    }


    class Program
    {
   
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
