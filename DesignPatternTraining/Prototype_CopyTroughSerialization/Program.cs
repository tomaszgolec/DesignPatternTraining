﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using static System.Console;

namespace Prototype_CopyTroughSerialization
{

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream,self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms,self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }

    }
   
    [Serializable]
    public class Person
    { 
        public string[] Names;
        public Address Address;

        public Person()
        {
            
        }

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
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

       
    }

    [Serializable]
    public class Address
    { 
        public string StreetName;
        public int HouseNumber;

        public Address()
        {
            
        }
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
            var john = new Person(new[] { "John", "Smith" },
                new Address("london road", 123));

            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;

            WriteLine(john);
            WriteLine(jane);
            ReadKey();
        }
    }
}
