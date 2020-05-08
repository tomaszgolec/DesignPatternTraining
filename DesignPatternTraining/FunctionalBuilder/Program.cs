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
    public class Person
    {
        public string Name, LastName , Position;
    }

    //this class can be reused everywhere actually
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public TSelf Called(string name)
            => Do(p => p.Name = name);

        public TSelf Do(Action<Person> action)
            => AddAction(action);

        public Person Build()
            => actions.Aggregate(new Person(), (p, f) => f(p));

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder
        : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    //    public PersonBuilder Called(string name)
    //        => Do(p => p.Name = name);

    //    public PersonBuilder Do(Action<Person> action)
    //        => AddAction(action);

    //    public Person Build()
    //        => actions.Aggregate(new Person(), (p, f) => f(p));

    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });
    //        return this;
    //    }

    //}

    //because of previous construction now we can add extension without modifing PersonBuilder claas (and this is open close principle example also)

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);

        public static PersonBuilder HisLastName(this PersonBuilder builder, string lastName)
            => builder.Do(p => p.LastName = lastName);
    }



    class Program
    {
        static void Main(string[] args)
        {
            Person person = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("DDeveloper")
                .HisLastName("Fafofwowiczocwczwoicz")
                .Build();
            ReadKey();
        }
    }
}
