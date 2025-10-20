namespace BattleSimulator1
{
    internal class Program
    {
        static string[] names = {"Jan", "Han", "Dan", "Josef", "Cyril",
                                "Vojtěch", "Jindřich", "Cecil", "Jakub", "Tomáš",
                                "Miroslav", "Zikmund", "Adam", "Vítězslav", "Petr",
                                "Jiří", "Samuel", "Svatopluk", "Štěpán", "Dominik"};
        static int counter = 0;
        static void Main(string[] args)
        {
            List<Character> army1 = CreateArmy();
            List<Character> army2 = CreateArmy();

            List<IMagic> magicians = new List<IMagic>();
                     
            if (army1[1] is IMagic)
                return;

            while(army1.Count>0 && army2.Count>0)
            {
                int min = Math.Min(army1.Count, army2.Count);
                for (int i = 0; i < min; i++)
                {
                    // army1[i].Attack2(army2[i]); // ukázka stejné funkčnosti odlišného přístupu 
                    army1[i].Attack(army2[i]);
                    army2[i].Attack(army1[i]);
                }
                RemoveCorps(army1);
                RemoveCorps(army2);
            }
        }

        static List<Character> CreateArmy()
        {
            List<Character> army = new List<Character>();
            Random rnd = new Random();

            for (int i = 0; i < rnd.Next(1, 4); i++)
            {
                army.Add(new Wizard(names[counter]));
                counter++;
            }
            for (int i = 0; i < rnd.Next(2, 6); i++)
            {
                army.Add(new Warrior(names[counter]));
                counter++;
            }
            int count = army.Count;
            for(int i = 0; i < 10 - count; i++)
            {
                army.Add(new Archer(names[counter]));
                counter++;
            }
            return army;
        }
        static void RemoveCorps(List<Character> army)
        {

            /*/ // => vyhodí Exception, že modifikujeme kolekci
            foreach (Character c in army)
            {
                if (!c.IsAlive())
                    army.Remove(c);
            }
            /**/

            /*/ // => přeskakuje některé členy armády, jelikož se neustále mění jejich indexy kvůli mazání
            for (int i = 0; i < army.Count; i++)
            {
                if (!army[i].IsAlive())
                    army.Remove(army[i]);

            }
            // toto by však šlo použít pozpátku - začít od posledního indexu
            /**/


            /*/
            // řešení pomocí nového listu - funkční, ale je potřeba použít při předávání amrády v parametru klíčové slovo "ref"
            List<Character> newArmy = new List<Character>();
            foreach (Character c in army)
            {
                if (c.IsAlive())
                    newArmy.Add(c);
            }
            army = newArmy;
            /**/

            /**/
            // nejčistší řešení
            army.RemoveAll(a => a.IsAlive() == false);
            /**/
        }
    }

    abstract class Character
    {
        public string Name { get;  }
        public int Health { get; protected set; }
        public int Power { get; protected set; }
        public Character(string name) 
        { 
            Name = name;
        }
        public override string ToString() 
        {
            return $"{this.GetType().Name} {Name} ({Health}/{Power})";
        }
        public virtual void Attack(Character target)
        {
            target.TakeDamage(Power);
            if (target.Health <= 0)
                Power++;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
        protected abstract void BattleScream();
        public void Attack2(Character target)
        {
            BattleScream();
            target.TakeDamage(Power);
            if (target.Health <= 0)
                Power++;
        }
    }

    class Wizard : Character, IMagic
    {
        public Wizard (string name) : base(name)
        {
            Health = 5;
            Power = 7;
        }
        public override void Attack(Character target)
        {
            Console.WriteLine("Za čaroděje!");
            base.Attack(target);
        }

        public void Spell()
        {
            throw new NotImplementedException();
        }

        protected override void BattleScream()
        {
            Console.WriteLine("Za čaroděje!");
        }
    }

    class Warrior : Character
    {
        public Warrior(string name) : base(name)
        {
            Health = 4;
            Power = 8;
        }
        public override void Attack(Character target)
        {
            Console.WriteLine("Za bojovníky!");
            base.Attack(target);
        }

        protected override void BattleScream()
        {
            Console.WriteLine("Za bojovníky!");
        }
    }

    class Archer : Character
    {
        public Archer(string name) : base(name)
        {
            Health = 7;
            Power = 3;
        }
        public override void Attack(Character target)
        {
            Console.WriteLine("Za lučištníky!");
            base.Attack(target);
        }

        protected override void BattleScream()
        {
            Console.WriteLine("Za lučištníky!");

        }
    }
    interface IPokus
    {

    }
    interface IMagic : IPokus
    {
        public void Spell();
    }
}
