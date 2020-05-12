using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using NUnit.Framework;
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

        //just for experimental purpose 
        private static int instanceCount;
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;
            WriteLine("initializing database");

            string fileLocation = Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
                "capitals.txt");

            capitals = File.ReadAllLines(fileLocation)
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

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }


    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db,Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count,Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] {"Seoul", "Mexico City"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000+17400000));
        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var db = SingletonDatabase.Instance;

    //        var city = "Tokyo";
    //        WriteLine($"{city} has population {db.GetPopulation(city)}");

    //        ReadKey();
    //    }
    //}
}
