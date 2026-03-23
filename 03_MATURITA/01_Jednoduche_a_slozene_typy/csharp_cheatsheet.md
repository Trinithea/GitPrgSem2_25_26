# C# --- Základní syntaxe (Cheatsheet)

------------------------------------------------------------------------

## 1. Základní datové typy a proměnné

### Číselné typy

``` csharp
int a = 10;          // celé číslo, zabírá 32 bitů (+- 2 miliardy)
uint b = 67890;      // nezáporné celé číslo (unsigned int), rozsah: 0 až 4 294 967 295
long c = 1300000000; // pro obrovská celá čísla, zabírá 64 bitů (+- 9 triliónů)
double b = 3.14;     // desetinné číslo (vyšší přesnost)
float c = 2.5f;      // desetinné číslo (nutné 'f')
long d = 1000000L;   // velké celé číslo
decimal e = 9.99m;   // finanční výpočty (vysoká přesnost)
```

### Text a znaky

``` csharp
string text = "Ahoj";
char znak = 'A';
```

### Logická hodnota

``` csharp
bool jeHotovo = true; // nebo false
```

### Automatické určení typu

``` csharp
var cislo = 5; // compiler určí typ (int)
```

------------------------------------------------------------------------

## 2. Funkce a metody

### Metoda bez parametrů
**Metoda**: Návratový typ je *void*, tedy nic se nevrací.
``` csharp
void Pozdrav()
{
    Console.WriteLine("Ahoj!");
}
```

### Metoda s parametry

``` csharp
void PozdravJmeno(string jmeno)
{
    Console.WriteLine($"Ahoj {jmeno}");
}
```

### Funkce s návratovou hodnotou
**Funkce**: Návratový typ je specifikován, něco se vrací.
``` csharp
int Secti(int a, int b)
{
    return a + b;
}
```

### Statická metoda
Ze statické metody (např. *Main()*) můžeme volat jen statickou metodu. Proto ve třídě *Program* píšeme statické metody.

``` csharp
static int Nasob(int a, int b)
{
    return a * b;
}
```

------------------------------------------------------------------------

## 3. Pole, List a Slovník

### Pole (Array)

-   pevná neměnná velikost - nutno určit při zakládání
-   rychlé, s indexy

``` csharp
int[] cisla = new int[4]; // vytvoří se pole o 4 nulových prvcích
```

nebo přímo definováním položek:

``` csharp
int[] cisla = { 1, 2, 3, 4 };
```
Délku pole získáme pomocí *Length*.
``` csharp
int delkaPole = cisla.Length;
```

Obsahuje seznam daný prvek?
``` csharp
bool jeTam = cisla.Contains(10);
```

Získání indexu dané položky (např. čísla 4):
``` csharp
int index = Array.IndexOf(cisla, 4);
```

#### Dvojrozměrné pole
``` csharp
// Pevná mřížka, všechny řádky musí mít stejnou délku.
        
// Inicializace: [řádky, sloupce]
int[,] matrix = new int[2, 3] { 
    { 1, 2, 3 }, 
    { 4, 5, 6 } 
};

// Přístup k prvku (řádek 0, sloupec 1)
int valueFromMatrix = matrix[0, 1]; // vrátí 2

// Změna hodnoty
matrix[1, 2] = 10;

// --- VÝPIS 2D POLE ---

// GetLength(0) vrací počet řádků, GetLength(1) počet sloupců
for (int i = 0; i < matrix.GetLength(0); i++)
{
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
        Console.Write(matrix[i, j] + "\t");
    }
    Console.WriteLine(); // Nový řádek po každém řádku pole
}
```

### List

-   dynamická velikost (může růst)
-   namespace: `System.Collections.Generic` - generická kolekce

``` csharp
List<int> seznam = new List<int>();

seznam.Add(10);
seznam.Add(20);
seznam.Remove(10);
```

Počet prvků v seznamu získáme pomocí *Count*.
``` csharp
int pocetPrvku = seznam.Count;
```

Obsahuje seznam daný prvek?
``` csharp
bool jeTam = seznam.Contains(10);
```

Získání indexu dané položky (např. čísla 10):
``` csharp
int index = seznam.IndexOf(10);
```

#### Dvojrozměrný list
``` csharp
// Dynamické, každý "řádek" může mít jiný počet prvků.

// Inicializace
List<List<int>> dynamicList = new List<List<int>>();

// Přidání "řádků"
dynamicList.Add(new List<int> { 10, 20 });
dynamicList.Add(new List<int> { 30, 40, 50, 60 }); // Delší řádek

// Přístup k prvku (seznam na indexu 1, prvek na indexu 2)
int valueFromList = dynamicList[1][2]; // vrátí 50

// Přidání prvku do konkrétního seznamu za běhu
dynamicList[0].Add(25);

// --- VÝPIS LISTU SEZNAMŮ (List<List<int>>) ---

// Používáme foreach, který je pro Listy velmi elegantní
foreach (var row in dynamicList)
{
    foreach (var item in row)
    {
        Console.Write(item + "\t");
    }
    Console.WriteLine(); // Nový řádek po vypsání jednoho vnitřního seznamu
}
```

### Slovník (Dictionary)

-   ukládá dvojice **klíč → hodnota**
- indexujeme podle **klíče**

``` csharp
Dictionary<string, int> veky = new Dictionary<string, int>();

veky.Add("Jan", 25);
veky["Petr"] = 30;
```

Přístup:

``` csharp
int vek = veky["Jan"];
```

Počet prvků ve slovníku:
``` csharp
int pocetPrvku = veky.Count;
```

Výpis slovníku (klíčů i hodnot):
``` csharp
foreach (var element in dictionary){
    Console.WriteLine($"Key: {element.Key}, Value: {element.Value}");
}
```

------------------------------------------------------------------------

## 4. Vlastní class a objekt

### Definice třídy

``` csharp
class Osoba
{
    public string Jmeno;
    public int Vek;

    public void PredstavSe()
    {
        Console.WriteLine($"Jmenuji se {Jmeno}");
    }
}
```

### Vytvoření objektu

``` csharp
Osoba o = new Osoba();
o.Jmeno = "Karel";
o.Vek = 30;

o.PredstavSe();
```

------------------------------------------------------------------------

## 5. Podmínky

### if / else if / else

``` csharp
int vek = 18;

if (vek < 18)
{
    Console.WriteLine("Nezletilý");
}
else if (vek == 18)
{
    Console.WriteLine("Právě plnoletý");
}
else
{
    Console.WriteLine("Dospělý");
}
```

### switch

``` csharp
int den = 2;

switch (den)
{
    case 1:
        Console.WriteLine("Pondělí");
        break;
    case 2:
        Console.WriteLine("Úterý");
        break;
    default:
        Console.WriteLine("Jiný den");
        break;
}
```

------------------------------------------------------------------------

## 6. Cykly

### while
- dokud platí podmínka
``` csharp
int i = 0;

while (i < 5)
{
    Console.WriteLine(i);
    i++;
}
```

### for
- když známe počet opakování
- *i* se nazývá iterační proměnná, jeden průchod je iterace
``` csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
```

### foreach
- vhodné pro procházení listů, slovníků, ...
``` csharp
List<int> cisla = { 1, 2, 3 };

foreach (int cislo in cisla)
{
    Console.WriteLine(cislo);
}
```

------------------------------------------------------------------------

## 7. Čtení vstupu

Načtení stringu:
``` csharp
string vstup = Console.ReadLine();
```

Načtení řady čísel oddělených mezerami:
``` csharp
string[] vstup = Console.ReadLine().Split();
int[] cisla = new int[vstup.Length];

for (int i = 0; i < vstup.Length; i++)
{
    cisla[i] = Convert.ToInt32(vstup[i]);
}

```
nebo kratším způsobem:
``` csharp
coins = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
```

------------------------------------------------------------------------

## Rychlé tipy

-   `;` ukončuje příkaz
-   `{ }` označují blok kódu - omezují existenci proměnných
-   C# je **silně typovaný jazyk**
-   `using System;` je potřeba pro `Console`

------------------------------------------------------------------------
