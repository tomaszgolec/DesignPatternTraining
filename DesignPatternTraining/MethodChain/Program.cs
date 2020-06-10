using System.Text.RegularExpressions;
using static System.Console;

namespace MethodChain
{
    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}, {nameof(Name)}: {Name}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; //linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature;
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null) next.Add(cm);
            else next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreasedDefenseModifier : CreatureModifier
    {
        public IncreasedDefenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            //if you apply this modifier then is not possible to apply more modifiers
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin",2,2);
            WriteLine(goblin);

            var root = new CreatureModifier(goblin);

            //root.Add(new NoBonusesModifier(goblin));

            WriteLine("Let's double the goblin's attack");
            root.Add(new DoubleAttackModifier(goblin));

            WriteLine("Lets increase goblin's defense");
            root.Add(new IncreasedDefenseModifier(goblin));

            root.Handle();
            WriteLine(goblin);


            ReadKey();
        }
    }
}
