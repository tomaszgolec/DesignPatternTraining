using System;
using System.Collections.Generic;
using static System.Console;

namespace AbstractFactory
{

    public interface IHotDrink
    {
        void Consume();
    }

    //internal is important, we dont wont expose this outside the library
    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("this tea is nice but i'd prefer it with milk.");
        }
    }
    
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This coffee is sensational!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    //we have two factories because is possible that we will have a lot of complicated methods which will be create different types of tea
    //and this same situation with coffee
    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal  class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
            Coffee,
            Tea
        }

        private Dictionary<AvailableDrink,IHotDrinkFactory> factories = 
            new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                //AbstracFactory is namespace in this case
                var factory = (IHotDrinkFactory) Activator.CreateInstance(
                    Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
                );

                factories.Add(drink,factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();

            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();

            var drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 100);
            drink2.Consume();

            ReadKey();
        }
    }
}
