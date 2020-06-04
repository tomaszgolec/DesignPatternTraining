using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace Tests
{

    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);

                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;//index of last element
                }
            }
            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ",
            names.Select(i => strings[i]).ToArray());
    }


    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestUser() //6851854
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User($"{firstName}{lastName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });

        }

        [Test]
        public void TestUser2() //7265434 - based on tutorial it should be less but isn't. 
        //i don't know why
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User2>();

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User2($"{firstName}{lastName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });

        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                    .Select(i => (char)('a' + rand.Next(26)))
                    .ToArray());
        }
    }
}