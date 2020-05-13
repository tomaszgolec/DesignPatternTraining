using System.Reflection.Metadata;
using Autofac;
using static System.Console;

namespace BridgePattern
{

    interface IRenderer
    {
        void RenderCircle(float radius);
    }

    class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing a circle of radius {radius}");
        }

    }

    class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }
        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //IRenderer renderer = new RasterRenderer();
            //IRenderer renderer = new VectorRenderer();
            //var circle = new Circle(renderer,5);


            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();

            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>()
                .SingleInstance();

            cb.Register((c, p) => 
                new Circle(c.Resolve<IRenderer>(),
                    p.Positional<float>(0)));

            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(
                    new PositionalParameter(0,5.0f)
                );


                circle.Draw();
                circle.Resize(2.0f);
                circle.Draw();
            }


            ReadKey();
        }
    }
}
