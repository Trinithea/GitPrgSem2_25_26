using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hodnotove_a_referencni_typy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --- 0. Hodnotové a referenční datové typy ---

            //      A) Hodnotové typy - ty jednodušší, ukládáme je přímo na volací zásobník
            //          - př. bool, byte, int, long, float, double, char, struktury (struct)
            //      B) Referenční typy - ty složitější, co mohou zabírat více paměti; na zásobník uložíme pouze odkaz (referenci) vedoucí na adresu na haldě, kde jsou uložená samotná data
            //          - př. string, array, list, objekty tříd


            #region 1. Kopírování hodnoty vs. odkazu

            int a = 10;      // hodnotový typ
            int b = a;       // kopie hodnoty
            b = 20;          // měníme jen kopii

            Console.WriteLine($"a = {a}, b = {b}");
            #region řešení
            // Výstup: a = 10, b = 20
            // int je hodnotový typ → a a b jsou nezávislé kopie.
            #endregion

            int[] list1 = new int[] { 1, 2, 3 }; // referenční typ (pole)
            int[] list2 = list1;                 // kopíruje se odkaz, ne data
            list2[0] = 99;

            Console.WriteLine($"list1[0] = {list1[0]}, list2[0] = {list2[0]}");
            #region řešení
            // Výstup: list1[0] = 99, list2[0] = 99
            // int[] je referenční typ → list1 a list2 odkazují na stejné místo v paměti.
            #endregion

            #region Ověření
            char ch1 = 'a';
            char ch2 = ch1;
            ch1 = 'b';
            Console.WriteLine($"ch1 = {ch1}, ch2 = {ch2}");
            #region
            // Výstup: ch1 = b, ch2 = a
            // char je hodnotový typ → ch1 a ch2 jsou nezávislé kopie.
            #endregion
            #endregion

            #region Záludné ověření
            string s1 = "ahoj";
            string s2 = s1;
            s1 = "nazdar";
            Console.WriteLine($"s1 = {s1}, s2 = {s2}");
            #region
            // Výstup: s1 = nazdar, s2 = ahoj
            // string je sice referenční typ ale neměnitelný - při změně se vytváří odkaz jinam do paměti → s1 a s2 odkazují na různá místa v paměti.
            #endregion
            #endregion
            #endregion


            #region 2. Předávání do metody/funkce
            int x = 5;
            ChangeValue(x);
            Console.WriteLine($"Po volání ChangeValue: x = {x}");
            #region řešení
            // 5
            // int je hodnotový typ → při předání se kopíruje hodnota.
            #endregion

            Person person = new Person { Name = "Alice" };
            ChangeReference(person);
            Console.WriteLine($"Po volání ChangeReference: {person.Name}");
            #region řešení
            // Bob
            // Person je referenční typ → metoda mění obsah objektu na daném místě v paměti.
            // Při předání v parametru se vytvoří lokální kopie odkazu do paměti.
            #endregion

            #region Ověření
            List<int> cisla = new List<int>() { 1, 2, 3 };
            ChangeList(cisla);
            Console.WriteLine($"Po volání ChangeList: {cisla[0]}");
            #region řešení
            // 0
            // List je referenční typ → metoda mění obsah daného místa v paměti.
            #endregion
            #endregion

            #region Záludné ověření
            Person person2 = new Person { Name = "Jana", Vek = 27 };
            ChangeValue(person2.Vek);
            Console.WriteLine($"Po volání ChangeValue: {person2.Vek}");
            #region řešení
            // 27
            // Přestože věk závisí na referenční proměnné person2, je hodnotového typu a tedy se do funkce předala pouze kopie
            #endregion
            #endregion
            #endregion


            #region 3. Změna reference uvnitř metody

            Person p = new Person { Name = "Alice" };
            ChangeReference2(p);
            Console.WriteLine(p.Name);
            #region řešení
            // Alice — nezměnilo se
            // Bez ref metoda mění jen lokální kopii odkazu.
            #endregion

            ChangeReferenceRef(ref p);
            Console.WriteLine(p.Name);
            #region řešení
            // Charlie — změnilo se
            // S ref metoda mění samotný odkaz volající proměnné (nevytváří se lokální kopie odkazu).
            #endregion

            #region Ověření
            List<int> cisla2 = new List<int>() { 1, 2, 3 };
            ChangeList2(cisla2);
            Console.WriteLine($"Po volání ChangeList2: {cisla2[0]}");
            #region řešení
            // 1
            // List je referenční typ, ale bez ref metoda mění jen lokální kopii odkazu.
            #endregion
            #endregion
            #endregion


            #region 4. Struktura x třída
            PointStruct ps1 = new PointStruct { X = 1, Y = 1 };
            PointStruct ps2 = ps1;  
            ps2.X = 9;

            Console.WriteLine($"Struct: ps1.X = {ps1.X}, ps2.X = {ps2.X}");
            #region řešení
            // Struct: ps1.X = 1, ps2.X = 9
            // struct = hodnotový typ → každá proměnná má vlastní kopii dat.
            #endregion

            PointClass pc1 = new PointClass { X = 1, Y = 1 };
            PointClass pc2 = pc1;  
            pc2.X = 9;

            Console.WriteLine($"Class: pc1.X = {pc1.X}, pc2.X = {pc2.X}");
            #region řešení
            // Class: pc1.X = 9, pc2.X = 9
            // class = referenční typ → proměnné sdílí stejný objekt.
            #endregion
            #endregion

            #region Doplnění: Static
            // statické funkce a vlastnosti
            //      nejsou vázány na insatnci (objekt) ale na třídu 
            //      mohou volat pouze statické funkce
            //      hodí se, když víme, že nebudeme potřebovat od dané třídy více instancí
            #endregion
        }

        static void ChangeValue(int value)
        {
            value = 10;
            #region Vysvětlení 
            // mění se pouze lokální kopie, kterou funkce nevrací => hodnoty proměnných mimo funkci zůstanou nezměněné
            #endregion
        }

        static void ChangeList(List<int> list)
        {
            list[0] = 0;
        }

        static void ChangeReference(Person p)
        {
            p.Name = "Bob"; // mění se objekt, na který odkazuje p
        }

        static void ChangeReference2(Person person)
        {
            person = new Person { Name = "Bob" }; // nová reference – změna se neprojeví venku
        }

        static void ChangeList2(List<int> list)
        {
            list = new List<int>() { 4,5,6 }; 
        }

        static void ChangeReferenceRef(ref Person person)
        {
            person = new Person { Name = "Charlie" }; // mění se reference samotná
        }
    }
    class Person
    {
        public string Name { get; set; }
        public int Vek { get; set; }
    }

    struct PointStruct
    {
        public int X, Y;
    }

    class PointClass
    {
        public int X, Y;
    }

}
