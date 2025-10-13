namespace OOP_Zaklady
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog("Rex");
            dog.Eat();  // zděděno z Animal
            dog.Bark(); // vlastní metoda

            Animal pes = new Dog("Rehor");
            pes.Speak();
            //pes.Bark(); // Kompilátor vnímá proměnnou pes jako Animal, kde Bark není definováno
                        // buď přetypujeme ((Dog)pes).Bark()
                        //nebo pattern matching
            if (pes is Dog d)
            {
                d.Bark();
            }


            Animal[] animals = { new Dog("Pepa"), new Cat("Micka") };

            foreach (var a in animals)
            {
                Console.WriteLine(a);
                a.Speak(); // každý typ se chová jinak
                a.Attack();
            }

            Shape s1 = new Circle { Radius = 3 };
            Shape s2 = new Rectangle { Width = 4, Height = 5 };

            Console.WriteLine(s1.GetArea()); // kruh
            Console.WriteLine(s2.GetArea()); // obdélník

        }
    }

    public class BankAccount
    {
        private decimal balance; // skrytý stav

        public void Deposit(decimal amount)
        {
            if (amount > 0)
                balance += amount;
        }

        public decimal Balance
        {
            get { return balance; }  // přístup přes getter
            private set { balance = value; } // chráněný setter
        }
    }

    public class Animal
    {
        public Animal(string name)
        {
            Name = name;
        }
        protected string Name; // chráněný přístup – dědic má přístup, ale vnější svět ne


        public void Eat()
        {
            Console.WriteLine($"{Name} jí.");
        }

        public virtual void Speak()
        {
            Console.WriteLine("Zvíře vydává zvuk.");
        }

        public virtual void Attack()
        {
            Console.WriteLine("Bojový pokřik");
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name) 
        { 
        
        }

        

        public void Bark()
        {
            Console.WriteLine($"{Name} říká: Haf!");
        }
        public override void Speak()
        {
            Console.WriteLine("Haf!");
        }
        public override void Attack()
        {
            Console.WriteLine("Vrrr!");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name) : base(name) { }
        public override void Speak()
        {
            Console.WriteLine("Mňau!");
            Attack();
        }
    }

    public abstract class Shape
    {
        public abstract double GetArea();
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }
        public override double GetArea() => Math.PI * Radius * Radius;
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override double GetArea() => Width * Height;
    }

    
}
