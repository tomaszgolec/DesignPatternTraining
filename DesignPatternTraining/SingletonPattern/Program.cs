using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using static System.Console;

namespace SingletonPattern
{

    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        private SingletonDatabase()
        {
            WriteLine("initializing database");

            capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(), 
                    list => int.Parse(list.ElementAt(1))
                    );

        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        //we can do this like this below and its ok, but
        //private static SingletonDatabase instanse = new SingletonDatabase();
        //if we make something like this, then instance will be created only in the moment when you try to us instance
        //so it will be lazy loading
        private  static  Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
    
        public static SingletonDatabase Instance => instance.Value;
    }

    class Program
    {

        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;

            var city = "Tokyo";
            WriteLine($"{city} has population {db.GetPopulation(city)}");

            ReadKey();
        }
    }
}
