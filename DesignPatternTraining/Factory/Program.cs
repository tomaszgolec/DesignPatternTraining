using System;
using static System.Console;

namespace Factory
{
    //based ont SRP we would like to move creation of objects to separate class because creation is different responsibility 
    //we can use factory methods in small classes when is small amount of this kind of methods

   

    public class Point
    {
        //factory method design pattern

        private double x, y;

        //if we use internal we avoid inappropriate usage of this constructor for clients using this library
        //but still someone can us it in wrong way inside library 
        //so good idea is put factory class inside this class and then we can keep constructor private! :)
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static Point Origin => new Point(0,0); //this is a property  

        public static Point Origin2 = new Point(0,0); //this is better because is initialising once but if you need always new object ten you can use line above

        public static class Factory
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

        //if is some reason why factory cannot be static then you can use alternatively solution below
        //but better approach is static, if you need static version then probably you hav bad design 
        //public  static PointFactory Factory => new PointFactory();
        //public class PointFactory
        //{
        //    public Point NewCartesianPoint(double x, double y)
        //    {
        //        return new Point(x, y);
        //    }

        //    public Point NewPolarPoint(double rho, double theta)
        //    {
        //        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        //    }
        //}
    }


    class Program
    {

        static void Main(string[] args)
        {
            //this patter is just API improvement similarly to builder
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);

            WriteLine(point);
            ReadKey();
        }
    }
}
