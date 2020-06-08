using System.Collections.Generic;
using static System.Console;

namespace CompositeProxy_SoA_AoS
{
    public class Creature
    {
        public byte Age;
        public int X, Y;
    }

    public class Creatures
    {
        private readonly int size;
        private byte[] age;

        private int[] x, y;

        public Creatures(int size)
        {
            this.size = size;
            age = new byte[size];
            x = new int[size];
            y = new int[size];
        }

        public struct CreatureProxy
        {
            private readonly Creatures _creatures;
            private readonly int _index;

            public CreatureProxy(Creatures creatures, int index)
            {
                _creatures = creatures;
                _index = index;
            }

            public ref byte Age => ref _creatures.age[_index];
            public ref int X => ref _creatures.x[_index];
            public ref int Y => ref _creatures.y[_index];
        }

        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < size; pos++)
            {
                yield return new CreatureProxy(this,pos);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //AoS
            var creatures = new Creature[100];

            // Age X Y Age X Y Age X Y

            // Age Age Age Age
            // X X X X
            // Y Y Y Y
            
            foreach (var c in creatures)
            {
                c.X++;
            }

            //AOS/SOA duality
            var creatures2 = new Creatures(100);
            // the main profit is better efficiency of this approach
            // more info in video
            foreach (Creatures.CreatureProxy c in creatures2)
            {
                c.X++;
            }

            ReadKey();
        }
    }
}
