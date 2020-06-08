using static System.Console;

namespace ProtectionProxy
{
    public  interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            WriteLine("Car is being driven");
        }
    }

    public class Driver
    {
        public int Age { get; set; }

        public Driver(int age)
        {
            Age = age;
        }
    }


    public class CarProxy : ICar
    {
        private Driver driver;
        private  Car car = new Car();

        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }

        public void Drive()
        {
            if (driver.Age >= 16)
            {
                car.Drive();
            }
            else
            {
                WriteLine("too young");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICar car = new CarProxy(new Driver(12));

            car.Drive();

            ReadKey();
        }
    }
}
