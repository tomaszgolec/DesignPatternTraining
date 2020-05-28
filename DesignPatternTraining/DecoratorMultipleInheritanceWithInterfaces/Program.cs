using System.Runtime.InteropServices.WindowsRuntime;
using static System.Console;

namespace DecoratorMultipleInheritanceWithInterfaces
{
    public interface IBird
    {
        void Fly();
        int Weight { get; set; }
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizard
    {
        void Crawl();
        int Weight { get; set; }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        private int weight;

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }

        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                lizard.Weight = value;
                bird.Weight = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 100;
            d.Fly();
            d.Crawl();
            ReadKey();
        }
    }
}
