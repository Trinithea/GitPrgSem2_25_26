# **Listy**

## **Add(T)**
*Popis:*
Přidá objekt T z parametru na konec listu.
*Časová komplexita:*
O(1) pokud kapacita se vejde do kapacity listu. O(n) pokud přidáním nového prvku přesáhne kapacitu listu a misí ji zvýšit tím, že vytvoří nový list s dvojnásobnou kapacitou.

## **Contains(T)**
*Popis:*
Prohledá list a zjistí jestli se T nenachází v listu. Vrátí true nebo false.
*Časová komplexita:*
O(n) - n je počet prvků v listě. Postupně projde kaźdý prvek v listu a zjistí jestli se jedná o prvek T.

## **Insert(Int32, T)**
*Popis:*
Vloží prvek T na zadanou pozici v listu.
*Časová komplexita:*
O(n) - n je počet prvků v listu. Prochází postupně pozice kam má nový prvek vložit a posune pozice ostatních prvků za ním.

## **Remove(T)**
*Popis:*
Prohledá list jestli se v něm nenachází prvek T a odstraní jej. Dále vrací true nebo false, podle toho jestli byl prvek úspešně odstraněn, nebo ne.
*Časová komplexita:*
O(n) - n je počert prvků v listu. Postupně prochází všechny prvky v listě dokud jeden neodstraní.

## **Rozdíl mezi List<T> a LinkedList<T>**

**List<T>** je array typu T s prázdnými poli vyhrazené pro přidávání nových prvků. Kdykoliv má být přídán nový prvek, v paměti se vytvoří, nový, dvojnásobně velký array a nahradí ten starý. Tím se "prodlouží" list. Narozdíl od Linked Listu je to jeden velký jednotný blok dat v paměti.

**Linked** list je několik samostatných prvků v paměti, který mají "odkaz" kde se v paměti náchází předchozí a další prvek v listu a tvoří tak takový řetěz. Je vhodný pro práci s daty, kde se často mění velokost listu.

Zdroje: oficiální dokumentace na learn.microsoft.com[1](https://learn.microsoft.com/cs-cz/dotnet/api/system.collections.generic.linkedlist-1?view=net-8.0)[2](https://learn.microsoft.com/cs-cz/dotnet/api/system.collections.generic.list-1?view=net-8.0)


program pro Nejmenší možnou kružnici
```csharp
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSkola
{
    class Program
    {
        public static List<point> points = new List<point>();
        static void Main(string[] args)
        {
            //Vytvoří náhodné číslo N a následně vygeneruje N počet bodů na kartézké soustavě souřadnice s hodnatami x a y od 0 do 320
            Random rnd = new Random();
            int N = rnd.Next(4, 11);
            for (int i = 0; i < N; i++)
            {
                points.Add(new point(rnd.Next(0, 320), rnd.Next(0, 320)));
                Console.WriteLine("Bod " + (i+1) +": "+points[i].X + " " + points[i].Y);
            }
            //začne welzův algoritmus, výsledkem je nejmenší možná kružnice W
            circle w = Welzl(points, new List<point>(), points.Count);
            Console.WriteLine("Střed: "+ w.Center.X + " " + w.Center.Y + " Poloměr: " + w.Radius);
            //Kontrola pro vypsání souřadnic
        }
        //Metoda kontroluje, jestli kružnice vzniklá na konci rekurze je platná, připadně jestli jestli ji lze definovat 2 nebo 3 bodama
        private static circle WelzFinnish(List<point> P)
        {
            if (P.Count < 2)
            {
                return new circle(new point(0,0), 0);
            }
            //Pokud jsme vyřadili Welzem všechny aź na 2 body
            if (P.Count == 2)
            {
                return circle.PointsToCircle(P[0], P[1]);
            }
            //Kontroluje jestli má na kružnici stačí 2 nebo 3 body
            //Zkusí udělat všechny možné kružnice ze vśech zbylích bodů a zkontroluje, jetsli se do ní ostati body vejdou
            //Pokud to lze, vrátí danou kružnici
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    circle c = circle.PointsToCircle(P[i], P[j]);
                    if (c.isValid(P))
                    {
                        return c;
                    }
                }
            }
            return circle.PointsToCircle(P[0], P[1], P[2]);
        }
        //Welzův algoritmus0
        //N je kolikrát musí rekurzí projít (za každý bod jednou)
        //P je původní list bodů
        //R je list bodů které se mají nacházet na kraji kružnice
        private static circle Welzl(List<point> P, List<point> kraj, int n)
        {
            if (n == 0 || kraj.Count == 3)
            {
                return WelzFinnish(kraj);
            }
            // Vezme náhodný bod
            int rand = new Random().Next(0, n+1);
            point p = P[rand];

            // Nahradí onen náhodný bod bodem z konce listu (protoźe kdyź ho jenom vymažu tak to tam dělá guláš)
            P[rand] = P[n - 1];

            // zopakuje funkci pro všechny body - náhodný bod
            //rekurzí  pak teda nejdřív najde pro 1, 2, 3....
            circle d = Welzl(P, kraj, n - 1);

            // zkontroluje, jestli kružnice pro vśechny - náhodný bod
            //poku jo, kružnice je v pořádku a vrátí
            if (d.contains(p))
            {
                return d;
            }

            // pokud ne, přidá bod do listu bodů na okraji kružnice 
            kraj.Add(p);

            // A zopakuje pro funkci pro nevý krajní list
            return Welzl(P, kraj, n - 1);
        }

       
    }
    class point
    {
        public double X, Y;
        public point(double x, double y)
        {
            X = x;
            Y = y;
        }
        //statická funkce pro nalezení vydálenosti dvou bodů
        public static double distance (point p1, point p2){
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }
    }
    class circle
    {
        public point Center;
        public double Radius;
        public circle(point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        // Funkce která vytvoří k třem bodů kružnici
        // Prakticky funguje podobně jako kdyby program tvořil kružnici opsanou k trojúhelníky z právě těch třech daných bodů
        public static circle PointsToCircle(point a, point b, point c)
        {
            //černá magie:
            double bx = b.X - a.X;
            double by = b.Y - a.Y;
            double cx = c.X - a.X;
            double cy = c.Y - a.Y;
           
            double B = bx * bx + by * by;
            double C = cx * cx + cy * cy;
            double D = bx * cy - by * cx;

            point s = new point((cy * B - by * C) / (2 * D), (bx * C - cx * B) / (2 * D));

            s.X += a.X;
            s.Y += a.Y;

            return new circle(s, point.distance(s, a));
        }
        //Vezme si 2 body a najde kružnici, které jsou oba body tětivou
        public static circle PointsToCircle(point A, point B)
        {
            point c = new point((A.X + B.X) / 2.0, (A.Y + B.Y) / 2.0);
            return new circle(c, point.distance(A, B) / 2.0);
        }

        //funkce pro kontrolu, jestli všechny body v listu jsou součástí kružnice (nestatická metoda)
        public bool isValid(List<point> points) {
        foreach (point p in points)
            {
                if (point.distance(p, Center) > Radius)
                {
                    return false;
                }
            }
            return true;
        }
        //fuknce která kontroluje, jestli se bod nachází v kružnici
        public bool contains(point Point)
        {
            return Radius >= point.distance(Center, Point);
        }
    }
}
```