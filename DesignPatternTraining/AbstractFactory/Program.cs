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
        //public enum AvailableDrink
        //{
        //    Coffee,
        //    Tea
        //}

        //private Dictionary<AvailableDrink,IHotDrinkFactory> factories = 
        //    new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        //AbstracFactory is namespace in this case
        //        var factory = (IHotDrinkFactory) Activator.CreateInstance(
        //            Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
        //        );

        //        factories.Add(drink,factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}


        //approach above is not so bad but the OCP is broken
        //actually we can use in this case dependency injection container but 
        //we just show how to do it with reflection

        private List<Tuple<string,IHotDrinkFactory>> factories = 
            new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                    !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory",string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available drinks");
            for (var index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;

                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factories.Count)
                {
                    Write("Specify amount: ");
                    s = ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0
                    )
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }

                WriteLine("Incorrect input, try again");
            }

            return null;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();
            ReadKey();
        }
    }
}
