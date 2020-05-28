using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using System.Xml.Serialization.Advanced;
using static System.Console;

namespace CompositeSpecification
{
    class Program
    {

        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Yuge
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

        //public interface ISpecification<T>
        //{
        //    bool IsSatisfied(T t);
        //}

        public abstract class ISpecification<T>
        {
            public abstract bool IsSatisfied(T p);

            public static ISpecification<T> operator &(
                ISpecification<T> first, ISpecification<T> second)
            {
                return new AndSpecification<T>(first,second);
            }
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public override bool IsSatisfied(Product t)
            {
                return t.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public override bool IsSatisfied(Product t)
            {
                return t.Size == size;
            }
        }

        public abstract class CompositeSpecification<T> : ISpecification<T>
        {
            protected readonly ISpecification<T>[] items;
            public CompositeSpecification(params ISpecification<T>[] items)
            {
                this.items = items;
            }
        }

        // comibnator
        public class AndSpecification<T> : CompositeSpecification<T>
        {
            public AndSpecification(params ISpecification<T>[] items) : base(items)
            {
            }

            public override bool IsSatisfied(T t)
            {
                return items.All(i => i.IsSatisfied(t));
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var i in items)
                    if (spec.IsSatisfied(i))
                        yield return i;
            }
        }

        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            WriteLine("Green Products (old):");

            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                WriteLine($" - {p.Name} is green");
            }

            var bf = new BetterFilter();
            WriteLine("Green Products (new):");

            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($" - {p.Name} is green");
            }


            WriteLine("Large blue items");

            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Blue) & new SizeSpecification(Size.Large)))
            {
                WriteLine($" - {p.Name} is big and blue");
            }

            ReadKey();
        }
    }
}
