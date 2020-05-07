using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SingleResponsibilityPrinciple
{
    class Program
    {
        public class Journal
        {
            private  readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count; // this is memento pattern
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

           
        }

        public class Persistence
        {
            public void SaveToFile(Journal j, string fileName, bool overwrite = false)
            {
                if (overwrite || !File.Exists((fileName)))
                    File.WriteAllText(fileName,j.ToString());
            }
        }

        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("i ate a bug");
            WriteLine(j);

            var p = new Persistence();
            var fileName = @"C:\temp\journal.txt";
            p.SaveToFile(j,fileName,true);
            Process.Start(fileName);

            ReadKey();
        }
    }
}
