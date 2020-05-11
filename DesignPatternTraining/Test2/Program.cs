using System.Runtime.InteropServices.ComTypes;
using static System.Console;

namespace Test2
{
    class Program
    {

        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class PersonFactory
        {
            private static int idCounter = 0;
            public Person CreatePerson(string name)
            {
                return new Person {Id = idCounter++, Name = name};
            }
        }

        static void Main(string[] args)
        {
            var pf = new PersonFactory();
            var person = pf.CreatePerson("jan");
            var person2 = pf.CreatePerson("jan2");

            ReadKey();
        }
    }
}
