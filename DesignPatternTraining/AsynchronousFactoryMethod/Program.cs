using System.Threading.Tasks;
using static System.Console;

namespace AsynchronousFactoryMethod
{
    public class Foo
    {

        //we cannot make constructor async, so if we myst make something asynchronously during creation of the object
        //then we must use asynchronous factory method like below
        private Foo()
        {
            //
        }

        private async Task<Foo> InitAsync()
        {
            //async process who must be out of the constructor
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Foo x = await Foo.CreateAsync();
            ReadKey();
        }
    }
}
