namespace Rozklad_na_scitance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadejte přirozené číslo: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Rozloz(n);
        }

        static void Rozloz(int n)
        {
            // zásobník bude obsahovat jednotlivé sčítance, tedy např. pro n=5: [1,1,1,1,1] nebo [1,1,1,2] nebo [1,1,3] nebo [1, 2, 2] nebo [1,4] nebo [2,3] nebo [5] 
            Stack<int> stack = new Stack<int>();

            // naplníme zásobník n jedničkami
            for (int i = 0; i < n; i++)
                stack.Push(1);
            int stackSum = n; // aktuální součet položek v zásobníku
            VypisZasobnik(stack);

            while (true)
            {
                int num = stack.Pop(); // odebereme vršek zásobníku

                if (num == n) break; // jestliže v zásobníku bylo zadané n, tak končíme

                stackSum -= num; // pakliže nebylo, pokračujeme v rozkladu a aktualizujeme součet 

                // důležité pozorování: jestliže jsme něco odebrali z vršku zásobníku, znamená to, že má cenu zkoušet jen čísla vyšší než vypopnuté číslo
                // proto zkoušíme opakovaně přidávat num + 1, ale jen dokud součet na zásobníku nepřesáhne dané n
                while (stackSum + num + 1 <= n) 
                {
                    stack.Push(num + 1); 
                    stackSum += num + 1;
                }

                // pokud se přidáváním podařilo získat takový stav zásobníku, jehož součet je roven n, jedná se o stav ještě neviděný a stojí za to jej vypsat
                if(stackSum == n) 
                    VypisZasobnik(stack);
            }
        }

        static void VypisZasobnik(Stack<int> stack)
        {
            Console.WriteLine(string.Join(" + ", stack.Reverse())); // Reverse čistě z estetických důvodů, aby byly jednotlivé sčítance v součtu vypsány vzepstupně
        }
    }
}
