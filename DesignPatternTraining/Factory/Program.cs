using System;
using static System.Console;

namespace Factory
{
    //based ont SRP we would like to move creation of objects to separate class because creation is different responsibility 
    //we can use factory methods in small classes when is small amount of this kind of methods

    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    public class Point
    {
        //factory method design pattern

        private double x, y;

        //is private now
        public Point(double x, double y)
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
            var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);

            WriteLine(point);
            ReadKey();
        }
    }
}
