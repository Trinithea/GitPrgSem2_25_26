# Funkce v `List<T>`

| Funkce      | Co dělá  | Časová složitost   | Odůvodnění č.s.   | Vliv pořadí |
|-------------|-----------|------------------------------|---------------------|-------------------------------------|
| **Add**     | Přidává prvek na konec seznamu | Průměrná O(1), ale může být O(n) | Typicky se prvek vloží rovnou na konec; při překročení kapacity se seznam zkopíruje → O(n). | Vždy přidává prvek na konec → žádný vliv na ČS. |
| **Contains**| Ověřuje, zda daný prvek existuje v seznamu (vrací True nebo False)   | O(n)   | Seznam se prohlédává postupně; v nejhorším případě (prvek je poslední nebo tam vůbec není) musí projít všechny prvky → O(n). | Pro první prvek O(1), pro poslední O(n) a pro něco uprostřed průměrně O(n/2). |
| **Insert**  | Přidává prvek na specifickou pozici v seznamu  | O(n)  | Po vložení prvku se všechny prvky za ním posunou doprava, takže program musí projít všechny. | Pro první prvek O(n), pro poslední O(1) (jako funkce Add) a pro něco uprostřed průměrně O(n/2). |
| **Remove**  | Odstraňuje první výskyt specifikovaného prvku ze seznamu  | O(n)   | Po odstranění prvku se všechny prvky před ním posunou doleva, takže program musí projít všechny. | Pro všechny stejné (musí projít celý seznam, posunout celý seznam nebo mix obojího). |

# Spojový seznam (Linked List)

Jedná se o lineární datovou strukturu, která se skládá z "uzlů" (nodes) propojených buď "ukazateli" (pointers) - C, C++, nebo referencemi - Java, Python, JavaScript. Jednotlivé uzly obsahují hodnotu a ukazatel/referenci na další uzel. Povoluje přidání i odebrání z jakékoliv pozice.

## Rozdíly mezi Linked List a `LinkedList<T>` v C#

- **Typ**: Linked List obvykle nemá typovou bezpečnost, oproti tomu v `LinkedList<T>` v C# máme umožněno definovat typ dat.
- **Struktura**: Linked List může mít různé struktury, `LinkedList<T>` v C# je předem definovaný (dvoucestný spojený seznam).
- **Funkce**: Linked List nemá předdefinované funkce pro běžné operace → může vyžadovat manuální implementaci operací.
- **Výkon** (vyplývá z předchozího bodu): V `LinkedList<T>` v C# jsou operace předem definované na nejnižší možnou časovou složitost, při vytváření vlastních funkcí v Linked List se nám to nemusí vždy podařit.

**SHRNUTÍ**: Obecný Linked List je různorodá datová struktura, oproti tomu `LinkedList<T>` v C# je konkrétní implementace.

## Vybrané funkce v obecném Linked List

| Funkce  | Co dělá   | Časová složitost | Odůvodnění č.s   |
|----------|-----------|------------------|--------------------|
| **Insert at head** | Přidání uzlu na začátek seznamu   | O(1) | Vyžaduje přístup pouze k "hlavě" (= první pozice).  |
| **Insert at tail**  | Přidání uzlu na konec seznamu  | O(1) / O(n)| Záleží, jestli je seznam jednosměrný nebo dvoucestný. U jednosměrného je potřeba projet seznam až do konce, u dvoucestného je to prakticky opačný insert at head. |
| **Insert at position**   | Přidání uzlu na specifickou pozici  | O(n) | Je potřeba projít seznam až do požadované pozice → lineární počet operací. |
| **Delete from head** | Odstranění uzlu ze začátku seznamu | O(1) | Vyžaduje přístup pouze k "hlavě" (= první pozice).         |
| **Delete from tail**    | Odstranění uzlu z konce seznamu | O(1) / O(n) | Záleží, jestli je seznam jednosměrný nebo dvoucestný. U jednosměrného je potřeba projet seznam až do konce, u dvoucestného je to prakticky opačný delete from head. |
| **Delete from position** | Odstranění uzlu z specifické pozice       | O(n) | Je potřeba projít seznam až do požadované pozice → lineární počet operací. |
| **Search**  | Vyhledávání uzlu  | O(n) | Je potřeba projít seznam pro porovnání hodnoty. |
| **Display**  | Vyzobrazení všech hodnot | O(n) | Pro zobrazení všech hodnot v seznamu musíme projít všechny uzly. |

# Zdroje
https://www.geeksforgeeks.org/linked-list-data-structure/
https://www.simplilearn.com/tutorials/data-structure-tutorial/linked-list-in-data-structure
https://www.geeksforgeeks.org/when-should-i-use-a-list-vs-a-linkedlist/
https://www.geeksforgeeks.org/c-sharp-removing-a-range-of-elements-from-the-list/
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1?view=net-7.0
https://chatgpt.com/
# ✧* KÓD ✧*
```csharp 
using System;

namespace ArraySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LookingForIntegral());
        }

        static int LookingForIntegral()
        {
            Console.WriteLine("Zadejte čísla do pole oddělená čárkami (např. 1, 3, 8):");
            string vstup = Console.ReadLine();

            Console.WriteLine("Hledaný integer: ");
            string hledany = Console.ReadLine();

            string[] hodnoty = vstup.Split(',');
            int[] integrals = new int[hodnoty.Length];

            for (int i = 0; i < hodnoty.Length; i++)
            {
                integrals[i] = int.Parse(hodnoty[i].Trim()); //Parse - nově hodnata int, Trim - vymaže bíle znaky (můžou zkazit nejen kod, ale i mentalní stav, to mě naučil loňský závěrečný projekt)
            }

            if (!int.TryParse(hledany, out int hledanyInteger)) // TryParse zkouší jestli hledany je číslo, pokud ano, uloží se do nově vytvořené proměnné hledanyInteger, pokud není, vrá
            {
                Console.WriteLine("Hledaný prvek není platné číslo 😠");
                return; 
            }

            for (int i = 0; i < integrals.Length; i++)
            {
                if (integrals[i] == hledanyInteger)
                {
                    return i; 
                }
            }
            return -1; 
        }
    }
}