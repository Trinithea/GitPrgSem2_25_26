//using namespace; -> díky tomu můžeme propojovat projekty mezi sebou


// NAMESAPACE - jmenný prostor, ve kterém definujeme třídy
namespace VysvetleniZakladu
{   // složené závorky vymezují určité bloky kódu, omezují viditelnost a existenci tříd, funkcí, proměnných, ...


    // INTERNAL - tato třída je viditelná všude ve výsledném EXE souboru (tzv. assembly)
        // MODIFIKÁTORY PŘÍSTUPU (ACCESSIBILITY MODIFIERS)
        // public, private, protected, ... internal
        // public - viditelné všude
        // private - viditelné jen v dané třídě - defaultní nastavení (pokud není uvedeno, tak private)
        // -> ZAPOUZDŘENÍ (viditelnost určitých částí kódu)

    // CLASS: v C# je vše ve třídě, v třídě jsou vlasnotsi, funkce a datové položky
    internal class Program         
    {


        // STATIC - lze zavolat na samotné třídě bez vytvoření instance (viz dále příklad s třídou A)
            // ze statické metody/funkce můžeme volat pouze statickou metodu/funkci

        // VOID - návratový typ, void -> nevracíme nic -> mluvíme o METODĚ
            // pokud vracíme konkrétní datový typ -> mluvíme o FUNKCI 

        // MAIN - první metoda, která je hledána po spuštění programu -> nemazat!
            // C# NOTACE PRO POJMENOVÁNÍ
            // třídy, funkce, metody, veřejné vlastnosti: PascalCase notace
            // privátní a lokální proměnné:  camelCasel notace
            // v Pythonu: snake_case, v C# ne
        // STRING[] ARGS - kdybychom program spouštěli z terminálu, můžeme za jeho adresu dopsat i nějaké argumenty... to však řešit nebudeme

        static void Main(string[] args)
        {
            // vysvětlení rozdílu mezi statickou a nestatickou metodou

            // pro volání statické funkce nevytváříme instanci:
            A.Nazdar();
            Console.WriteLine(); 

            // pro volání nestatické funkce je potřeba vytvořit novou instanci
            A a = new A();
            a.Ahoj(); 

            A b = new A();
            b.Ahoj();

            // výhoda: instancí můžeme vytvářet neomezeně mnoho, každé můžeme jinak nastavit její vlastnosti a podle toho se bude i *dynamicky* měnit chování jejích funkcí (není *statické*)

        }

    }
    class A
    {
        public void Ahoj() { }

        public static void Nazdar() { }
    }
}

