using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Advanced;
using static System.Console;

namespace OpenClosedPrinciple
{
    class Program
    {

        public enum  Color
        {
            Red,Green, Blue
        }

        public enum Size
        {
            Small,Medium,Large,Yuge
        }

        public class Product
        {
            //this field are public but is only because this is example project
            public string Name;
            public Color Color;
            public Size Size;

            public Product(string name, Color color, Size size)
            {
                Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach (var p in products)
                {
                    if (p.Size == size)
                    {
                        yield return p;
                    }
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var p in products)
                {
                    if (p.Color == color)
                    {
                        yield return p;
                    }
                }
            }

            public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
            {
                foreach (var p in products)
                {
                    if (p.Size == size && p.Color == color)
                    {
                        yield return p;
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            var apple = new Product("Apple",Color.Green,Size.Small);
            var tree = new Product("Tree",Color.Green,Size.Large);
            var house = new Product("House",Color.Blue,Size.Large);

            Product[] products = {apple, tree, house};

            var pf = new ProductFilter();
            WriteLine("Green Products (old):");

            foreach (var p in pf.FilterByColor(products,Color.Green))
            {
                WriteLine($" - {p.Name} is green");
            }

            ReadKey();
        }
    }
}
