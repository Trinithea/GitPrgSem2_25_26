List:
Add:
- Přidá prvek na konec listu
- Časová složitost je v naprosté většině případů O(1), pouze pokud se list musí překopírovat, tak je složitost O(n)
- Časová složitost není závislá na umístění prvku, protože vždy přidává na konec

Contains:
- Prohledá seznam, aby zjistil, zda obsahuje daný prvek
- Časová složitost je O(n), protože funkce musí v nejhorším případě projít celý seznam, než najde požadovaný prvek (nebo zjistí, že tam není)
- Složitost není závislá na umístění prvku, přestože program může prvek najít dříve než projde celý list

Insert:
- Vloží prvek na zadanou pozici v seznamu.
- Časová složitost je O(n), protože v nejhorším případě (při vkládání na začátek seznamu) se musí posunout n prvků
- Čím blíž ke konci seznamu prvek přidáváme, tím míň prvků se musí posunout, takže o to méně časově náročné to je

Remove:
- Odebere první výskyt daného prvku ze seznamu
- Časová složitost je O(n), protože je nejprve nutné prvek najít a poté všechny prvky za ním posunout o jednu pozici zpět
- Nezáleží na tom, kde se prvek nachází, jelikož čím dříve ho najdeme tím více prvků musíme posunout

Linked list
- datová struktura skládající se z uzlů, kde každý uzel obsahuje data a odkaz na další uzel seznamu

LinkedList v c#
- V c#se ukládá jak následující tak předchozí uzel seznamu, což zefektivňuje navigaci v listu

Add: (first/last/before/after)
- Přidá prvek na začátek/konec seznamu
- Časová složitost je O(1), protože přidání prvku vyžaduje jen změnu ukazatele na první uzel

Remove: (first/last)
- Odebere první/poslední prvek ze seznamu
- Časová složitost je O(1), protože odebrání prvku vyžaduje jen změnu ukazatele

Remove:
- Odebere první výskyt zadané hodnoty
- Časová složitost je O(n) protože musí projít nejhůře celý seznam, aby prvek našel

Contains:
- Prohledá seznam, aby zjistil, zda obsahuje daný prvek
- Časová složitost je O(n), protože funkce musí v nejhorším případě projít celý seznam, než najde požadovaný prvek (nebo zjistí, že tam není)

Find:
- Vrátí první uzel obsahující daný prvek
- Časová složitost je O(n), protože funkce musí v nejhorším případě projít celý seznam, než najde požadovaný prvek (nebo zjistí, že tam není)

Zdroje:
- https://learn.microsoft.com/cs-cz/dotnet/api/system.collections.generic.linkedlist-1?view=net-8.0
- https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0

```csharp 
static int (int n, int array_lenght)
{
    int[] integers = new int[array_lenght];
    for (int i = 0, i < array_lenght, i++)
    {
        if (integers[i] == n)
        {
            return i;
        }
    }
    return -1;
}


```