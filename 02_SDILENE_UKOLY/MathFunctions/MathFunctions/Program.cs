using System;

namespace MathFunctions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<MathFunction> functions = new()
            {
                new LinearFunction(2, 3),
                new QuadraticFunction(1, -2, 1)
            };

            double x = 2;
            foreach (var f in functions)
            {
                f.PrintInfo();
                Console.WriteLine($"f({x}) = {f.Calculate(x)}\n");
                if (f is IDifferentiable)
                    Console.WriteLine(((IDifferentiable)f).OutputDerivative());
            }
        }
    }
    struct Interval
    {

        public double UpperBoundValue { get; }
        public double LowerBoundValue { get; }
        public char UpperBoundBracket { get; }
        public char LowerBoundBracket { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbb">Lower Bound Bracket</param>
        /// <param name="lbv">Lower Bound Value</param>
        /// <param name="ubv">Upper Bound Value</param>
        /// <param name="ubb">Upper Bound Bracket</param>
        public Interval(char lbb, double lbv, double ubv, char ubb)
        {
            UpperBoundBracket = ubb;
            LowerBoundBracket = lbb;
            UpperBoundValue = ubv;
            LowerBoundValue = lbv;
        }
        public override string ToString()
        {
            return $"{LowerBoundBracket}{LowerBoundValue},{UpperBoundValue}{UpperBoundBracket}";
        }
    }
    abstract class MathFunction
    {
        public string Name { get;  }
        public string Description { get;  }
        public Interval Domain { get; protected set; } // chtělo by to vlastně jako list intervalů... 
        public Interval Range { get; protected set; }

        public MathFunction(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract double Calculate(double x);

        public virtual void PrintInfo()
        {
            Console.WriteLine($"{Name}: {Description} na D(f) = {Domain}");
        }
    }

    class LinearFunction : MathFunction, IInvertible, IDifferentiable
    {
        private double a, b; // zapouzdření, privátní datové položky

        public LinearFunction(double a, double b) : base("Lineární funkce", $"f(x) = {a}x + {b}")
        {
            this.a = a;
            this.b = b;
            Domain = new Interval ('(',double.NegativeInfinity,double.PositiveInfinity,')');
            Range = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
        }

        public override double Calculate(double x) => a * x + b;

        public string OutputDerivative() =>  $"{a}";

        public string OutputInversion()
        {
            if (a == 0)
                return "Nelze, jelikož a = 0.";
            return $"f^(-1)(x) = (x - {b}) / {a} \n D(f^(-1)) = {Range}";
        }
    }

}
