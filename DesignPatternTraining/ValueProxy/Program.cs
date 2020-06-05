using System;
using System.Diagnostics;
using static System.Console;

namespace ValueProxy
{
    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage
    {
        private readonly float value;

        internal Percentage(float value)
        {
            this.value = value;
        }

        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }

        public static Percentage operator +(Percentage first, Percentage second)
        {
            return  new Percentage(first.value + second.value);
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }

    public static class ProcentageExtensions
    {
        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.0f);
        }
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0f);
        }

    }

    class Program
    {
       
        static void Main(string[] args)
        {
            WriteLine(
                10f* 5.Percent()
                );
            WriteLine(
                2.Percent() + 3.Percent() // 5%
                );

            ReadKey();
        }
    }
}
