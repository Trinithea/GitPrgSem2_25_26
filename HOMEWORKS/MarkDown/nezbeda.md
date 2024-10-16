#  n je počet prvků v listu

**Add:** 
- Přidá objekt na konec listu
- O(1) - pokud není potřeba zvýšit kapacitu listu, aby vyhovovala novému prvku, stane se z této metody operace O(n) - vytvoření listu s větší kapacitou O(1) a přepsání každého prvku ze starého listu do nového O(n)

**Contains:** 
- Určuje, zda je prvek v listu
- O(n) - zkontroluje nejhůře každý prvek jestli je v listu

**Insert:** 
- Vloží objekt do listu v zadaném indexu
- O(n) - vytvoří se nový list kam se přepíšou všechny prvky na nové indexy O(n) a přídá se tam nový zadaný prvek na zadaný index O(1)
- pokud se index = velikosti listu, tak funguje jako funkce Add - (čím vyšší index, tím menší obtížnost)

**Remove:** 
- Odebere první výskyt konkrétního prvku z listu
- O(n) - nejhůře projde všechny prvky při hledání zadaného objektu
- pokud se (index -1) = velikosti listu, tak obtížnost je O(1) - (čím vyšší index, tím menší obtížnost)

<br>

## Linked List
- je list, který je propojený , což znamená, že každý prvek v sobě uchovává informaci o prvku před ním a o prvku následujícím (odkaz na ně)
- přístup k prvku na konkrétní index stojí O(n), jelikož musí projít nejhůře celý seznam aby se na daný index dostal
- list se může zvětšovat a zmenšovat jak chce, jelikož se u prvků přidají/přepíší odkazy na ostatní prvky 
- přidávání a odebírání do/z listu je O(1) pokud máme odkaz na správné místo kam/odkud chceme prvek přidat/odebrat

<br>

### Zdroje
- [Microsoft](https://learn.microsoft.com/cs-cz/dotnet/api/system.collections.generic.list-1?view=net-8.0)

<br>
<br>

# Kód Funkce NajdiPrvek

```csharp
static int NajdiPrvek(List<int> cisla, int HledaneCislo)
{
    for (int i = 0; i < cisla.Count; i++)
    {
        if (cisla[i] == HledaneCislo)
        {
            return i;
        }
    }
    return -1;
}
```