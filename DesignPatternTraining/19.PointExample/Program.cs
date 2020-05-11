using System;
using static System.Console;

namespace FactoryMethod
{

    public class Point
    {
        //factory method design pattern
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho*Math.Cos(theta),rho*Math.Sin(theta));
        }

        private double x, y;

        //is private now
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }


    class Program
    {
   
        static void Main(string[] args)
        {
            //this patter is just API improvement similarly to builder
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);

            WriteLine(point);
            ReadKey();
        }
    }
}
