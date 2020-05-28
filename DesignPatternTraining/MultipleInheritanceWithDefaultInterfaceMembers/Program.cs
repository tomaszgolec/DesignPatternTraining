using static System.Console;

namespace MultipleInheritanceWithDefaultInterfaceMembers
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly() 
        {//default implementation, it works since c# 8 so here it does not work
            if(Age >= 10)
                WriteLine("I am flying");
        }
    }


    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if(Age < 10)
                WriteLine("I am crawling");
        }
    }

    public class Organism { }

    public class Dragon : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }

    //approaches to implement decorator
    // inheritance
    // SmartDragon(Dragon) - dependency injection
    // extension method 
    // C#8 default interface methods

    public class Program
    {
        static void Main(string[] args)
        {

            Dragon d = new Dragon {Age = 5};

            //in my opinion this is bad idea to use code like this
            //because we actually break Liskov Substitution Principle here

            if (d is IBird bird)
                bird.Fly();

            if (d is ILizard lizard)
                bird.Crawl();

            WriteLine("Hello World!");
        }
    }
}
