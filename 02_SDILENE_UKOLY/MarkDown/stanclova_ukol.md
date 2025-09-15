<center> .⋅˚₊‧ 🜲 ‧₊˚ ⋅</center>


<center>⎛⎝ ≽  >  ⩊   < ≼ ⎠⎞</center>
<center><span style="font-family: Babas; font-size: 2em;">✦•⋅⋯๑⋅⋯ LISTY & FUNKCE ⋅⋯๑⋅⋯•✦</span></center>


<center>˚　 ⋆⁺₊✦⁺₊ 　 ˚　.˚  .　 ☁.　　. 　 ˚　⁺⋆₊　.˚  .　. ✦⋆⁺₊  　 ˚　. ☁ ˚　.˚　 ✩₊˚.   ☾      ⋆      ⁺₊✧  　　˚　⁺₊ . 　 ˚　.　⁺₊✦</center>

<br>

## .˚₊ᗢ ʚ˚₊𝐀𝐃𝐃₊˚ʚ ᗢ₊˚✧ ﾟ.
#### Funkce: 
Přidá prvek (většinou) na konec seznamu.
#### Časová složitost: 
Amortizovaná časová složitost (neboli průměrně) je složitost O(1), protože prvek je přidán na konec seznamu a není potřeba přesouvat další prvky.
V nejhorším případě je složitost O(n), protože pole je plné a musí se zvětšit, což způsobí kopírování všech prvků do nového většího pole.
#### Příklad:
```C#
List<string> List = new List<string>()
    {
        "Pepa",
        "Sofi",
        "Honza"
    };
List.Add("Anička"); //přidá prvek Anička do seznamu 
                    //nyní list vypadá takto: {"Pepa", "Sofi", "Honza", "Anička"}
```

<br>

## .˚₊ᗢ ʚ˚₊𝐂𝐎𝐍𝐓𝐀𝐈𝐍𝐒₊˚ʚ ᗢ₊˚✧ ﾟ.     
#### Funkce: 
Prochází seznam a kontroluje, zda seznam obsahuje zadaný prvek. Pokud prvek nalezne, vypíše 'true', v opačném případě 'false'.
#### Časová složitost: 
V seznamech je obecně časová složitost O(n), protože je potenciálně nutné projít všechny prvky v seznamu.
Pokud bude prvek na začátku, pak se časová složitost zmenšuje, protože není nutné projít všechny prvky.
#### Příklad:
```C#
List<string> List = new List<string>()
    {
        "Pepa",
        "Sofi",
        "Honza"
    };
List.Contains("Sofi"); //hledá prvek Sofi v seznamu 
                       //pokud bychom vypsali, vyšlo by 'true'
```

<br>

## .˚₊ᗢ ʚ˚₊𝐈𝐍𝐒𝐄𝐑𝐓₊˚ʚ ᗢ₊˚✧ ﾟ.
#### Funkce: 
Vloží prvek do seznamu na určený/specifický index a posune tím všechny následující prvky.
#### Časová složitost: 
V průměru je časová složitost O(n), protože je nutné projít každý prvek za pozicí vloženého prvku.
V nejhorším případě (prvek je vložen hned na začátek seznamu) je časová složitost opět O(n), protože musíme projít všechny zbylé prvky v seznamu.
#### Příklad:
```C#
List<string> List = new List<string>()
    {
        "Pepa",
        "Sofi",
        "Honza"
    };
List.Insert(1, "Anička"); //přidá prvek Anička na pozici 1 do seznamu 
                          //nyní list vypadá takto: {"Pepa", "Anička", "Sofi", "Honza"}
```

<br>

## .˚₊ᗢ ʚ˚₊𝐑𝐄𝐌𝐎𝐕𝐄₊˚ʚ ᗢ₊˚✧ ﾟ.
#### Funkce: 
Odstraní první výskyt zadaného prvku v seznamu na základě hodnoty a posune tím všechny následující prvky o jedno místo.
#### Časová složitost: 
Průměrně je časová složitost O(n), protože je nutné projít seznam, než se nalezne daný prvek. Následně je nutné projít a posunout prvky za vymazaným prvkem, což je opět časová složitost O(n).
#### Příklad:
```C#
List<string> List = new List<string>()
    {
        "Pepa",
        "Sofi",
        "Honza"
    };
List.Remove("Sofi"); //odebere prvek Sofi ze seznamu 
                     //nyní list vypadá takto: {"Pepa", "Honza"}
```

<br>
<center>˚　 ⋆⁺₊✦⁺₊ 　 ˚　.˚  .　 ☁.　　. 　 ˚　⁺⋆₊　.˚  .　. ✦⋆⁺₊  　 ˚　. ☁ ˚　.˚　 ✩₊˚.   ☾      ⋆      ⁺₊✧  　　˚　⁺₊ . 　 ˚　.　⁺₊✦</center>
<br>

## .˚₊ᗢ ʚ˚₊𝐒𝐏𝐎𝐉𝐎𝐕Ý 𝐒𝐄𝐙𝐍𝐀𝐌₊˚ʚ ᗢ₊˚✧ ﾟ.
#### Obecně
Spojový seznam (neboli *"LinkedList"*) je lineární (prvky jsou uspořádány za sebou) datová struktura, kde každý prvek nese dva parametry - hodnotu a ukazatel na další prvek v seznamu. Je to jako "provaz" s "uzly", kde každý uzel má datové pole a odkaz na další "uzel". Prvky seznamu jsou v paměti umístěny na nesouvislých místech, takže program "skáče" v paměti pomocí ukazatelů a hledá takto prvky.

#### Druhy:
**Jednosměrný spojový seznam** (*"Singly Linked List"*): Je "řetězec", kde každý prvek (uzel) ukazuje (ukazatelem) na další prvek.
**Dvousměrný spojový seznam** (*"Doubly Linked List"*): "Řetězec", který je trochu více náročný na pamět, kde každý prvek má ukazatel na předchozí i následující prvek.

#### Zhodnocení spojového seznamu:
**Klady**: dynamická velikost (rozšiřování a zmenšování dle potřeby -> není nutné určovat velikost předem), rychlé vkládání a mazání, malá šance problémů s pamětí (prvky ukládány na různých místech v paměti), jednoduché rošiřování (přidávání nových uslů je rychlé a jednoduché)
**Zápory**: vyšší pamětová náročnost (každý uzel má ještě ukazatel, což zabírá místo navíc), pomalý přístup k prvkům (nutné procházet prvky jeden po druhém), nemožnost přístupu na základě indexu (prvky nemají indexy, maximálně lze použít "pointery"), složitost implementace

#### Rozdíl od LinkedListu v C#:
LinkedList<T.> v C# je konkrétní implementace spojového seznamu. Je dvousměrý a poskytuje typovou bezpečnost díky určenému typu, což zmenšuje chyby při běhu. Třída obsahuje předdefinované metody (jako např.: AddFirst, AddLast, Remove, ...). C# implementace je optimalizována pro .NET prostředí, což zajišťuje efektivní správu paměti a výkonu.

#### Funkce:
**Vkládání**:
| Operace      | Časová složitost |
| ----------- | ----------- |
| Přidej prvek na začátek   | O(1) |
| Přidej prvek na konec | O(1)/O(n) |
| Přidej prvek na konkrétní pozici   | O(n) |

Při přidávání prvku na začátek se pouze vytvoří nový uzel a aktualizuje ukazatel.
Pokud chceme p5idat prvek na konec, časová složitost závisí na tom, zda máme jednoduchý (musí se pojít celý seznam - O(n)) nebo dvojitý (pokud máme ukazatel od prvního na poslední prvek, pak O(1)) seznam. 
Při hledání konkrétní pozice musíme projít seznam, což je průměrně O(n).

<br>

**Odstranění**:
| Operace      | Časová složitost |
| ----------- | ----------- |
| Smaž prvek na začátku   | O(1) |
| Smaž prvek na konci | O(1)/O(n) |
| Smaž prvek na konkrétní pozici   | O(n) |

<br>

**Hledání**:
| Operace      | Časová složitost |
| ----------- | ----------- |
| Projdi a porovnej | O(n) |

Musíme projít celý seznam a porovnat s každou hodnotou, což dělá O(n).

<br>

**Procházení**:
| Operace      | Časová složitost |
| ----------- | ----------- |
| Projdi seznam | O(n) |

<br>

**Úprava**:
| Operace      | Časová složitost |
| ----------- | ----------- |
| Uprav hodnotu uzlu | O(n) |

<br>
<center>˚　 ⋆⁺₊✦⁺₊ 　 ˚　.˚  .　 ☁.　　. 　 ˚　⁺⋆₊　.˚  .　. ✦⋆⁺₊  　 ˚　. ☁ ˚　.˚　 ✩₊˚.   ☾      ⋆      ⁺₊✧  　　˚　⁺₊ . 　 ˚　.　⁺₊✦</center>
<br>

## .˚₊ᗢ ʚ˚₊𝐕𝐋𝐀𝐒𝐓𝐍Í 𝐈𝐌𝐏𝐋𝐄𝐌𝐄𝐍𝐓𝐀𝐂𝐄 𝐅𝐔𝐍𝐊𝐂𝐄₊˚ʚ ᗢ₊˚✧ ﾟ.
#### Funkce: 
V zadaném poli integerů (který přijme jako parametr) hledá zadaný prvek (který přijme jako druhý parametr) a vrací -1 nebo index, na kterém jej našla.

#### Implementace: 
```C#
namespace NevimNevim
{
    public class Program
    {
        public static void Main(string[] args)
        {
           List<int> InputNumbers = new List<int>();

           while (true)
           {
               Console.WriteLine("Zadejte číslo (jakmile nechcete již nic přidat, napište '-')");
               string input = Console.ReadLine();
               
               if (input == "-")
               {
                   break;
               }
               else
               {
                   if (int.TryParse(input, out int number)) //metoda přvádí input na číslo a dá ho do nové proměnné, pokud by se dalo třeba "abc", pak to hodí false
                   {
                       InputNumbers.Add(number);
                   }
                   else
                   {
                       Console.WriteLine("Neplatný vstup, zadejte číslo.");
                   }
               }
           }
           
           Console.WriteLine("Zadejte číslo, které chcete najít");
           string inputSearchNumber = Console.ReadLine();

           if (int.TryParse(inputSearchNumber, out int searchNumber))
           {
               bool IsItHere = InputNumbers.Contains(searchNumber);
               if (IsItHere) //pokud true
               {
                   Console.WriteLine("Kuk ale ono tu je");
                   int indexOfTheNumber = InputNumbers.IndexOf(searchNumber);
                   Console.WriteLine($"Číslo nalezeno na indexu {indexOfTheNumber}.");
               }
               
               else
               {
                   Console.WriteLine(":( není to tu...");
               }
               
           }
               
           else
           {
               Console.WriteLine("Neplatný vstup, zadejte číslo.");
           }           
        }
    }
}
```

<br>
<center>˚　 ⋆⁺₊✦⁺₊ 　 ˚　.˚  .　 ☁.　　. 　 ˚　⁺⋆₊　.˚  .　. ✦⋆⁺₊  　 ˚　. ☁ ˚　.˚　 ✩₊˚.   ☾      ⋆      ⁺₊✧  　　˚　⁺₊ . 　 ˚　.　⁺₊✦</center>
<br>

## .˚₊ᗢ ʚ˚₊𝐙𝐃𝐑𝐎𝐉𝐄₊˚ʚ ᗢ₊˚✧ ﾟ.
Zdroj 1: [Microsoft - List<T> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0)
Zdroj 2: [Algoritmy.net - Spojový seznam](https://www.algoritmy.net/article/24/Spojovy-seznam)
Zdroj 3: [ČVUT - Spojové struktury](https://cw.fel.cvut.cz/old/_media/courses/a0b36pr1/lectures/lecture10-handout-2x2.pdf)
Zdroj 4: [GeeksForGeeks - Linked List Implementation in C#](https://www.geeksforgeeks.org/linked-list-implementation-in-c-sharp/)
Zdroj 5: [Itnetwork.cz - Lekce 3 - Spojový seznam](https://www.itnetwork.cz/algoritmy/datove-struktury/spojovy-seznam)
List<T>.Add(T): [Microsoft - List<T>.Add(T) Method](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-8.0)
List<T>.Add(T): [SimpliLearn - Introduction to List in C#](https://www.simplilearn.com/tutorials/asp-dot-net-tutorial/c-sharp-list)
List<T>.Contains(T): [Microsoft - List<T>.Contains(T) Method](https://learn.microsoft.com/cs-cz/dotnet/api/system.collections.generic.list-1.contains?view=net-8.0)
List<T>.Contains(T): [GeeksForGeeks - How to check whether a List contains a specified element](https://www.geeksforgeeks.org/c-sharp-how-to-check-whether-a-list-contains-a-specified-element/)
List<T>.Insert(T): [Microsoft - List<T>.Insert(Int32, T) Method](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.insert?view=net-8.0)
List<T>.Insert(T): [StackOverflow - How to add item to the beginning of List<T>?](https://stackoverflow.com/questions/390491/how-to-add-item-to-the-beginning-of-listt)
List<T>.Remove(T): [Microsoft - List<T>.Remove(T) Method](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.remove?view=net-8.0)
List<T>.Remove(T): [StackOverflow - How to remove an object from List<T> in C# and return the removed object?](https://stackoverflow.com/questions/18142872/how-to-remove-an-object-from-listt-in-c-sharp-and-return-the-removed-object)


<p align="center">
    <img src="https://www.wfla.com/wp-content/uploads/sites/71/2023/05/GettyImages-1389862392.jpg?w=960&h=540&crop=1" alt="Popis obrázku" />
</p>