using System;
using static System.Console;

namespace Prorotype
{

    public class Person // ICloneable, we will not use ICloneable because is never clear is it deep or shallow copy , but this soulution
    //also will be very rare because is rare recognized in .net world, is mainly from c++
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            this.Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ",Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new []{"John","Smith"},
                new Address("london road",123));

            var jane = new Person(john);
            jane.Address.HouseNumber = 321;

            WriteLine(john);
            WriteLine(jane);
            ReadKey();
        }
    }
}
