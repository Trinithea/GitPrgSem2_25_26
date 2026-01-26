using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HraosPokus2.Model
{
    internal class Randomization<T>
    {
        // Partial Fisher–Yates
        public static IEnumerable<int> PickDistinct(int max, int count)
        {
            if (count > max)
                throw new ArgumentException("k cannot be greater than n");

            Random rng = new Random();
            int[] array = Enumerable.Range(1, max).ToArray();

            for (int i = 0; i < count; i++)
            {
                int j = rng.Next(i, max);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array.Take(count);
        }
    }
}
