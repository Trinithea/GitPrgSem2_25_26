using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropadavaciPiskvorky
{   

    internal class Program 
    {
        static void Main(string[] args)
        {
            // Main chceme mít co nejstručnější

            // zeptáme se hráče na parametry pro hru (inspirujte se rozšířeními)

            // podle toho vytvoříme hru
            Hra hra = new Hra(5,6,7,2); // voláme konstruktor při vytváření instance
            Hrac vitez = hra.Play(); // funkce Play vrací vítěze

            // vypište vítěze
        }

    }

    class Hra
    {
        //konstruktor - jeho parametry by měly obsahovat vše, co je potřeba nastavit při inicializaci herního objektu (jedné instanci hry)
        public Hra(int pocetVyhernichZetonu, int pocetSloupcu, int pocetRadku, int pocetHracu)
        {
            this.pocetVyhernichZetonu = pocetVyhernichZetonu; // použili jsme slovo this, protože se parametr konstruktoru nazývá stejně jako datová položka v této třídě
            hraciPole = new char[pocetSloupcu, pocetRadku];

            hraci = new Hrac[pocetHracu]; // vytvoří pole ale už nevytvoří samotné hráče! zatím jsou v poli jen samé null hodnoty
            VytvorHrace(); // hráči v poli se vytvoří až v této funkci

        }

        // následující datové položky jsou privátní - není potřeba, aby byly vidět mimo třídu
        private int pocetVyhernichZetonu; // datová položka
        private char[,] hraciPole;
        private Hrac[] hraci;

        private void VytvorHrace()
        {
            for (int i = 0; i < hraci.Length; i++)
            {
                Console.WriteLine($"Napiš jméno hráče {i}:");
                string jmeno = Console.ReadLine();
                Console.WriteLine($"Jaký symbol pro něj budeš chtít použít:");
                char symbol = Convert.ToChar(Console.ReadLine());

                hraci[i] = new Hrac(jmeno, symbol); // až zde dojde k reálnému vytvoření hráče v paměti (konkrétně na *haldě*)
            }
        }

        // hru ovšem musíme nějak spustit, je tedy public, aby šla spustit i ze třídy Program, vrací vítězného hráče
        public Hrac Play()
        {
            // TODO - následující úkoly si zaslouží vlastní funkci/metodu

            // dokud hra neskončí
                // vypište hráče, který je na řadě
                // vypište herní pole
                // zpracujte hráčův tah (např. zeptejte se do jakého sloupce chce vhodit žeton a podle toho na správné místo v hracím poli umístěte jeho žeton)
                // zkontrolujte, zda tímto tahem nevyhrál
                // pokud ne, je na řadě další hráč
                // pokud ano, vraťce vítěze
        }
        private bool Check(Position soucasnaPozice, Hrac hrac)
        {
            return CheckColumn(soucasnaPozice, hrac) || CheckRow(soucasnaPozice, hrac) || CheckDiag(soucasnaPozice, hrac);
        }

        private bool CheckColumn(Position soucasnaPozice, Hrac hrac)
        {
            return true; 
        }

        private bool CheckRow(Position soucasnaPozice, Hrac hrac)
        {
            return true;
        }

        private bool CheckDiag(Position soucasnaPozice, Hrac hrac)
        {
            return true;
        }

    }

    class Hrac
    {
        // Jméno i Symbol budeme využívat i v třídě Hra, proto jsou PUBLIC
        // Protože jsou public, tak je píšeme s velkým počátečním písmenem
        public string Jmeno { get; } 
        public char Symbol { get; }

        public Hrac(string jmeno, char symbol)
        {
            Jmeno = jmeno;
            Symbol = symbol;
        }
    }

    // struktura je jako třída, ale paměťově omezená, hodí se pro jednoduché datové typy jako například udržování dvojice souřadnic pro pozici ve hře
    struct Position 
    {
        public int Row;
        public int Column;
    }


    
}