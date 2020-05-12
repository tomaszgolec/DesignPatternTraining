using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FunctionalBuilder
{
    class Program
    {
        public class Person
        {
            public string Name, Position;
        }

        public sealed class PersonBuilder
        {
            public public List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

            public PersonBuilder Called(string name)
                => Do(p => p.Name = name);

            public PersonBuilder Do(Action<Person> action)
                => AddAction(action);

            public Person Build()
                => actions.Aggregate(new Person(), (p, f) => f(p));

            private PersonBuilder AddAction(Action<Person> actions)
            {
                

                //actions.Add(p =>
                //{
                //    action(p);
                //    return p;
                //});
                return this;
            }

        }

        //because of previous construction now we can add extension without modifing PersonBuilder claas (and this is open close principle example also)

        public static class PersonBuilderExtensions
        {
            public static PersonBuilder WorksAs
                (this PersonBuilder builder, string position)
                => builder.Do(p => p.Position = position);
        }


        static void Main(string[] args)
        {
            var person = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("DDeveloper")
                .Build();
            ReadKey();
        }
    }
}