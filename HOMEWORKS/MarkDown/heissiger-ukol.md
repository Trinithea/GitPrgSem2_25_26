Add: funguje, jako append v pythonu - přidá prvek do listu, složitost O(1)

Contains: vrátí boolean na základě zjištění, jestli v listu zkoumaný prvek je, nebo není, složitost O(délka_listu)

Insert: stejně jako Add, ale na zadanou pozici, O(délka_listu)

Remove: odstraní první najitý prvek shodný se zadaným, O(délka_listu)

Linked list: každý prvek má informaci o předchozím a následujícím prvku, jsou zde funkce AddBefore a AddAfter, které přidají prvek před/za určitý jiný (O(1)), Remove node - odstraní uzel (O(1))

zdroj: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0


```csharp 
\\ static int finding_position(List<int> list_cisel, int cislo)
{
    int pozice = -1;
    for (int i = 0; i < list_cisel.Count; i++)
    {
        if (list_cisel[i] == cislo)
        {
            pozice = i;
        }
    
    }
    return pozice;
}
```