using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using static System.Console;

namespace Observer_Via_Special_Interface
{

    public class Event
    {
        
    }

    class FallsIllEvent : Event
    {
        public string Address;
    }

    //that means that this class will generate event it something will change
    public class Person : IObservable<Event>
    {

        private readonly HashSet<Subscription> subscriptions
            =new HashSet<Subscription>();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this,observer);
            subscriptions.Add(subscription);
            return subscription;
        }

        public void FallsIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent{Address = "123 London Road"});
            }
        }

        private class  Subscription : IDisposable 
        {
            private readonly Person person;
            public  readonly IObserver<Event> Observer;

            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }
                
            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }


    public class Program : IObserver<Event>
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var person = new Person();

            //both line below do the same but in second example you don't must implement IObserver, you can just use simple
            //lambda expression, but you must add System.Reactive to usings
            IDisposable sub = person.Subscribe(this);

            person.OfType<FallsIllEvent>().Subscribe(args => Console.WriteLine($"A doctor is required at {args.Address}"));


            person.FallsIll();
            ReadKey();
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
           if (value is FallsIllEvent args)
                Console.WriteLine($"A doctor is required at {args.Address}");
        }
    }
}
