using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using namespace; -> díky tomu můžeme propojovat projekty mezi sebou


// namespace - jmenný prostor, ve kterém definujeme třídy
namespace ConnectGame
{   // složené závorky vymezují určité bloky kódu, omezují viditelnost a existenci tříd, funkcí, proměnných, ...


    // internal - tato třída je viditelná všude ve výsledném EXE souboru (tzv. assembly)
    // MODIFIKÁTORY PŘÍSTUPU (ACCESSIBILITY MODIFIERS)
    // public, private, protected, ... internal
        // public - viditelné všude
        // private - viditelné jen v dané třídě - defaultní nastavení (pokud není uvedeno, tak private)
    // -> ZAPOUZDŘENÍ (viditelnost určitých částí kódu)

    internal class Program // v C# je vše ve třídě, v třídě jsou vlasnotsi, funkce a datové položky
    {
        static void Main(string[] args)
        {
            // C# notace pro pojmenování
            // třídy, funkce, metody, veřejné vlastnosti: PascalCase notace
            // privátní a lokální proměnné:  camelCasel notace
            // v Pythonu: snake_case, v C# ne

            int i = 0;

            Console.WriteLine(); // statické funkce lze volat ppouze bez instance
            A a = new A();
            a.Ahoj(); // nestatické pouze na instanci
                      // A.Ahoj();


            int[,] board = new int[6, 7]{
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 2, 1, 0 },
                { 2, 2, 2, 2, 2, 1, 0 }
            };
            int[] position = { 5, 0 };
            // pozice řádek s indexem 5
            // pozice sloupec s indexem 0

            int hrac = 2;
            int pocetKamenuNaVyhru = 5;

            Hra hra1 = new Hra(5, 6, 7,2);
            // Hra hra2 = new Hra();


            hra1.Play();


            Console.WriteLine(hra1.Check(board, position, hrac, pocetKamenuNaVyhru));
            Console.ReadLine();

        }

    }

    class Hra
    {
        //konstruktor
        public Hra(int pocetVyhernichZetonu, int sirkaPole, int vyskaPole, int pocetHracu)
        {
            this.pocetVyhernichZetonu = pocetVyhernichZetonu;
            hraciPole = new int[sirkaPole, vyskaPole];
            hraci = new Hrac[pocetHracu];
        }

        int pocetVyhernichZetonu; // datová položka
        int[,] hraciPole;

        Hrac[] hraci;
        public Hrac Play()
        {
            Hrac hrac = new Hrac(); 
            Position startPosition = new Position();
            Console.WriteLine("Na řadě je hráč", hrac.Jmeno);
            startPosition.Row = 0;
            startPosition.Column = 0;


            // Tah
            // Check
            //střídání hračů
        }
        public bool Check(int[,] board, int[] soucasnaPozice, int hrac, int pocetKamenuNaVyhru)
        {
            int radek = soucasnaPozice[0];
            int sloupec = soucasnaPozice[1];
            return CheckColumn(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckRow(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckDiag(board, radek, sloupec, hrac, pocetKamenuNaVyhru);
        }

        bool CheckColumn(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 0;
            int pocetRadku = gameField.GetLength(0);

            for (int i = radek; i < pocetRadku; i++) 
            {
                if (gameField[i, sloupec] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin) 
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        bool CheckRow(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1; 
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = sloupec + 1; i < pocetSloupcu; i++)
            {
                if (gameField[radek, i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            
            for (int i = sloupec - 1; i >= 0; i--) 
            {
                if (gameField[radek, i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        bool CheckDiag(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1;
            int pocetRadku = gameField.GetLength(0);
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = 1; radek + i < pocetRadku && sloupec + i < pocetSloupcu; i++) 
            {
                if (gameField[radek + i, sloupec + i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek - i >= 0 && sloupec - i >= 0; i++) 
            {
                if (gameField[radek - i, sloupec - i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }


            pocet = 1; 


            for (int i = 1; radek - i >= 0 && sloupec + i < pocetSloupcu; i++) 
            {
                if (gameField[radek - i, sloupec + i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek + i < pocetRadku && sloupec - i >= 0; i++) 
            {
                if (gameField[radek + i, sloupec - i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }

    }

    class Hrac
    {
        public string Jmeno;
        public string Symbol;
    }
    struct Position
    {
        public int Row;
        public int Column;
    }


    class A
    {
        public void Ahoj()
        {

        }
    }
}