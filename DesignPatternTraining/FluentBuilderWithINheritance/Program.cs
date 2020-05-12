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

            public class Builder:PersonJobBuilder<Builder>
            {
                
            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }


        public abstract class PersonBuilder
        {
            protected Person person = new Person();
            public Person Build()
            {
                return person;
            }
        }
        public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
        {
            public SELF Called(string name)
            {
                person.Name = name;
                return (SELF) this;
            }
        }

        public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorksAsA(string position)
            {
                person.Position = position;
                return (SELF) this;
            }
        }

        static void Main(string[] args)
        {
            //here we have a problem because fluent builder does not work if you use this with inheritance

            var me =  Person.New
                .Called("dmitri")
                .WorksAsA("engineer")
                .Build();
            WriteLine(me);
            ReadKey();
        }
    }
}
