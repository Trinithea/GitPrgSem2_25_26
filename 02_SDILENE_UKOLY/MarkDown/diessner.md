Funkce Add | Funkce Contains | Funkce Insert | Funkce Remove
---------- | --------------- | ------------- | -------------
Přidá na konec listu položku. | Zjistí, zda je konkrétní prvek v seznamu. | Přidá položku na konkrétní pozici v listu. | Odstraní danou položku.
Časová složitost O(1) pokud délka listu nepřesáhle kapacitu listu. | Časová složitost O(n) kde n je délka listu. Funkce musí projít maximálně celý seznam. | Časová složitost O(n) kde n je délka seznamu. Funkce musí přeindexovat maximálně n prvků. | Časová složitost O(n) kde n je délka listu. Funkce musí projít maximálně celý list, než najde položku k smazání.
Neliší se v zavislosti s jakým prvkem pracujeme. | Liší se v závislosti s jakým prvkem pracujeme. Čím nižší je index hledaného prvku, tím nižší je obtižnost. | Liší se v závislosti s jakým prvkem pracujeme. Čím vyšší je index hledaného prvku, tím nižší je obtižnost. | Liší se v závislosti s jakým prvkem pracujeme. Čím vyšší je index hledaného prvku, tím nižší je obtižnost.

<br>
<br>

##### Linked List je seznam propojený uzly. V C# má zároveň každý prvek informaci o následujícím a předchozím prvku. 
##### Nabízí funkce Add a Remove s časovou O(1). Funkcí Add jdou ale v tomto případě přidávat prvky jen před nebo za nějaký existující prvek nebo na začátek či konec listu. Funkcí remove lze odstranit prvky z konce, či začátku listu, anebo konkrétní prvek.
##### Zdroje: 
https://learn.microsoft.com/en-us/dotnet/csharp/
https://www.geeksforgeeks.org/csharp-programming-language/?ref=outind

<br>
<br>

#### Funkce Najdi Číslo
```csharp 
static void NajdiCislo(List<int> list, int a)
{
    for (int i = 0; i < list.Count; i++)
    {
        if (list[i] == a)
        { 
            Console.WriteLine(i); 
            break;
        }
        

    }
    Console.ReadLine();
}
```