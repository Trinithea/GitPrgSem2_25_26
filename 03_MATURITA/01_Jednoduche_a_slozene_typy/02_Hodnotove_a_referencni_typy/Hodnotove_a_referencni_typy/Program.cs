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

            int[] pole1 = new int[] { 1, 2, 3 }; // referenční typ (pole)
            int[] pole2 = pole1;                 // kopíruje se odkaz, ne data
            pole2[0] = 99;

            Console.WriteLine($"list1[0] = {pole1[0]}, list2[0] = {pole2[0]}");
            #region řešení
            // Výstup: list1[0] = 99, list2[0] = 99
            // int[] je referenční typ → list1 a list2 odkazují na stejné pole.
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
            // Person je referenční typ → metoda mění obsah objektu, ne odkaz.
            #endregion

            int y = 5;
            ChangeRefValue(ref y);
            Console.WriteLine($"Po volání ChangeRefValue: y = {y}");
            #region řešení
            // 10
            // int je hodnotový typ, ale přidali jsme ref → při předáváme odkaz na místo, kde proměnná leží.
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
            // S ref metoda mění samotný odkaz volající proměnné.
            #endregion
            #endregion


            #region 4. Struktura x třída
            PointStruct ps1 = new PointStruct { X = 1, Y = 1 };
            PointStruct ps2 = ps1;  // kopie hodnot
            ps2.X = 9;

            Console.WriteLine($"Struct: ps1.X = {ps1.X}, ps2.X = {ps2.X}");
            #region řešení
            // Struct: ps1.X = 1, ps2.X = 9
            // struct = hodnotový typ → každá proměnná má vlastní kopii dat.
            #endregion

            PointClass pc1 = new PointClass { X = 1, Y = 1 };
            PointClass pc2 = pc1;  // kopíruje se odkaz
            pc2.X = 9;

            Console.WriteLine($"Class: pc1.X = {pc1.X}, pc2.X = {pc2.X}");
            #region řešení
            // Class: pc1.X = 9, pc2.X = 9
            // class = referenční typ → proměnné sdílí stejný objekt.
            #endregion
                       
            #endregion
        }

        static void ChangeValue(int value)
        {
            value = 10;
            #region vysvětlení
            // mění se lokální kopie
            #endregion
        }

        static void ChangeRefValue(ref int value)
        {
            value = 10;
            #region vysvětlení
            // mění se místo pod odkazem
            #endregion
        }

        static void ChangeReference(Person p)
        {
            p.Name = "Bob";
            #region vysvětlení
            // mění se objekt, na který odkazuje p
            #endregion
        }

        static void ChangeReference2(Person person)
        {
            person = new Person { Name = "Bob" };
            #region vysvětlení
            // nová reference – změna se neprojeví venku
            #endregion
        }

        static void ChangeReferenceRef(ref Person person)
        {
            person = new Person { Name = "Charlie" };
            #region vysvětlení
            // mění se reference samotná
            #endregion

        }
    }
    class Person
    {
        public string Name { get; set; }
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
