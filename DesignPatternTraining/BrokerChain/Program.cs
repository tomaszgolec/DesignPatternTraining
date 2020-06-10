using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace BrokerChain
{
    //mediator pattern
    //event broker
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender,q);
        }
    }

    public class Query
    {
        public string CreatureName;
        public enum Argument
        {
            Attack, Defense
        }

        public Argument WhatToQuery;
        public int Value;

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName;
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public class Creature
    {
        private Game game;
        public string Name;
        private int attack, defense;

        public Creature(Game game, string name, int attack, int defense)
        {
            this.game = game;
            Name = name;
            this.attack = attack;
            this.defense = defense;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name,Query.Argument.Attack,attack);
                game.PerformQuery(this,q);
                return q.Value;
            }
        }

        public int Deffense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(attack)}: {attack}, {nameof(Attack)}: {Attack}, {nameof(defense)}: {defense}, {nameof(Deffense)}: {Deffense}, {nameof(game)}: {game}, {nameof(Name)}: {Name}";
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected Game game;
        protected Creature creature;

        protected CreatureModifier(Game game, Creature creature)
        {
            this.game = game;
            this.creature = creature;
            game.Queries += Handle;
        }

        protected abstract void Handle(object sender,Query q);

        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackerModifier : CreatureModifier
    {
        public DoubleAttackerModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
                && q.WhatToQuery == Query.Argument.Attack)
                q.Value *= 2;
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
                && q.WhatToQuery == Query.Argument.Defense)
                q.Value += 2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var goblin = new Creature(game,"strong goblin",3,3);
            WriteLine(goblin);

            using (new DoubleAttackerModifier(game,goblin))
            {
                WriteLine(goblin);
                using (new IncreaseDefenseModifier(game, goblin))
                {
                    WriteLine(goblin);
                    
                }
            }

            WriteLine(goblin);
            ReadKey();
        }
    }
}
