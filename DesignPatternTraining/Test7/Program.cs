using static System.Console;

namespace Test7
{

    public abstract class Shape
    {
        public string Name { get; set; }
    }


    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class Triangle : Shape
    {
        private IRenderer renderer;
        public Triangle(IRenderer renderer)
        {
            this.renderer = renderer;
            Name = "Triangle";
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }
    }

    public class Square : Shape
    {
        private readonly IRenderer renderer;

        public Square(IRenderer renderer)
        {
            this.renderer = renderer;
            Name = "Square";
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }
    }


    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "pixels";
    }


    //we have an old approach below, on first sight bridge pattern above can looks like unnecessary
    //but if you will imagine that we have 10 implementations of shape and for each, we have the next two implementation
    //like below then we will have more than 20 classes(int bridge pattern we have only 12) and worst is we must
    //change implementation in every class which inherit from square or other classes
    //even if we have the same function inside so BRIDGE PATTERN IS GOOD APPROACH WHEN WE HAVE MULTIPLE CLASSES WITH
    //CARTESIAN-PRODUCT DUPLICATION, OBVIOUSLY IT WILL BE GOOD SOLUTION EVEN IN ADVANCE
    //public class VectorSquare : Square
    //{
    //    public override string ToString() => "Drawing {Name} as lines";
    //}

    //public class RasterSquare : Square
    //{
    //    public override string ToString() => "Drawing {Name} as pixels";
    //}

    // imagine VectorTriangle and RasterTriangle are here too
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");
        }
    }
}
