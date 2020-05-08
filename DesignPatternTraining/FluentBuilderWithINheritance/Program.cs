using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FluentBuilderWithINheritance
{
    class Program
    {

        public class Person
        {
            public string Name;
            public string Position;

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public class PersonInfoBuilder
        {
            protected Person person = new Person();

            public PersonInfoBuilder Called(string name)
            {
                person.Name = name;
                return this;
            }
        }

        public class PersonJobBuilder : PersonInfoBuilder 
        {
            public PersonJobBuilder WorksAsA(string position)
            {
                person.Position = position;
                return this;
            }
        }

        static void Main(string[] args)
        {
            //here we have a problem because fluent builder does not work if you use this with inheritance
            var builder = new PersonJobBuilder();
            builder.Called("dmitri")
                .WorksAsA

            ReadKey();
        }
    }
}
