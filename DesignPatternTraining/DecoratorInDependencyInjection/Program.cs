using Autofac;
using static System.Console;

namespace DecoratorInDependencyInjection
{
    class Program
    {

        public interface IReportingService
        {
            void Report();
        }

        public class ReportingService : IReportingService
        {
            public void Report()
            {
                WriteLine("Here is your report");
            }
        }

        public class ReportingServiceWithLogging : IReportingService
        {
            private IReportingService decorated;

            public ReportingServiceWithLogging(IReportingService decorated)
            {
                this.decorated = decorated;
            }
            public void Report()
            {
                WriteLine("Commencing log log...");
                decorated.Report();
                WriteLine("Ending log...");
            }
        }

        static void Main(string[] args)
        {

            var b = new ContainerBuilder();

            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");

            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service), "reporting");

            using (var c = b.Build())
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }

            ReadKey();
        }
    }
}
