using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InterfaceSegregationPrinciple
{
    class Program
    {
        public class Document
        {
            
        }

        public interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        public class MultifunctionalPrinter : IMachine
        {           
            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }

            public void Fax(Document d)
            {
               //
            }
        }

        public class OldFashionPrinter :  IMachine
        {
            public void Print(Document d)
            {
               //
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }

            public void Fax(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public class Photocopier : IPrinter,IScanner
        {
            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
               //
            }
        }

        public interface IMultiFunctionDevice  : IScanner,IPrinter
        {
            
        }

        public class MultiFunctionMachine : IMultiFunctionDevice
        {
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                this.printer = printer;
                this.scanner = scanner;
            }
            
            public void Print(Document d)
            {
                printer.Print(d);
            }

            public void Scan(Document d)
            {
                scanner.Scan(d);
            }//dectorator pattern
        }

        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}
