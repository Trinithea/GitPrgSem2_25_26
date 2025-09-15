using System.Globalization;
using System.Text;

namespace UvodniUkol_Filmy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //nastavíme kódování výstupní konzole na UTF-8, aby se zobrazila hvězdička

            // vyvtvoříme 3 instance od třídy Film
            // v kulatých závorkách voláme konstruktor
            // novou instanci vytváříme slovem "new" a to v paměti na tzv. haldě.
            Film film1 = new Film("Princezna nevěsta", "Rob", "Reiner", 1987);
            Film film2 = new Film("Hvězdný prach", "Matthew", "Vaughn", 2007);
            Film film3 = new Film("Pějme píseň dohola", "Ondřej", "Trojan", 1990);


            // uložíme je do listu filmů
            List<Film> filmy = new List<Film>(); //nejprve si založíme daný list s položkami typu Film
            filmy.Add(film1);
            filmy.Add(film2);   
            filmy.Add(film3);

            // přidáme 15 náhodných hodnocení každému filmu
            Random rnd = new Random();
            foreach (Film film in filmy)
            {
                for (int i = 0; i < 15; i++)
                {
                    film.PridejHodnoceni(5*rnd.NextDouble());
                }
            }

            // najdeme film s nejlepším hodnocením a nejdelším názvem
            Film nejlepsiHodnoceniFilm = filmy[0];
            
            Film nejdelsiNazevFilm = filmy[0];

            foreach(Film film in filmy)
            {
                if (film.Hodnoceni > nejlepsiHodnoceniFilm.Hodnoceni)
                {
                    nejlepsiHodnoceniFilm = film;
                    
                }

                if (film.Nazev.Length > nejdelsiNazevFilm.Nazev.Length)
                {
                    nejdelsiNazevFilm = film;
                }
            }
            Console.WriteLine(nejlepsiHodnoceniFilm);
            Console.WriteLine(nejdelsiNazevFilm);


            // vypíšeme vše, co je odpad
            foreach(Film film in filmy)
            {
                if (film.Hodnoceni < 3)
                    Console.WriteLine($"{film.Nazev} je odpad! Má hodnocení jen {film.Hodnoceni}.");
            }

            // výpis všech filmů 
            foreach (Film film in filmy)
                Console.WriteLine(film); 
                    // WriteLine automaticky zavolá .ToString() a přidá nakonec i odřádkování 
            
        }
    }
    
    class Film
    {
        // notace v C#:
        //      PascalCase (pro public záležitosti a názvy tříd) 
        //      camelCase (pro private záležitosti)

        // konstruktor třídy Film - volá se vždy při vytvoření nové instance
            // jde o metodu, která nic nevrací (void vynecháváme) a jemnuje se stejně jako třída, je vždy veřejný
            // každá třída má automaticky vlastní bezparametrický konstruktor, my ho však takto přepíšeme na parametrický
            // v konstruktoru nastvaíme vše, co je potřeba nastavit pro plnou funkčnost dané instance
        public Film(string nazev, string jmenoRezisera, string prijmeniRezisera, int rokVzniku)
        {
            Nazev = nazev;
            JmenoRezisera = jmenoRezisera;
            PrijmeniRezisera= prijmeniRezisera;
            RokVzniku = rokVzniku;

            Hodnoceni = null;
            dosavadniHodnoceni = new List<double>(); // inicializace
        }

        public string Nazev { get; }
            // public - tato vlastnost je viditelná i z jiných tříd
            // {get;} - tato vlastnost má pouze getter - lze pouze získat její hodnotu, nikoli ji měnit (read-only)
            // bez {get;} by šlo jen o datovou položku nikoli o vlastnost a byla by zvenku měnitelná

        public string JmenoRezisera { get; }
        public string PrijmeniRezisera { get; }
        public int RokVzniku { get; }

        /// <summary>
        /// Celkové průměrné hodnocení. Pokud má hodnotu 'null', tak ještě nikdo žádné hodnocení nepřidal.
        /// </summary>
        public double? Hodnoceni { get; private set; }
        // přidali jsme i setter ovšem privátní - hodontu vlastnosti lze měnit pouze zevnitř třídy, kde je deifnovaná (Film)
        // zde by bylo lepší použít datový typ float (4 B) na hodnocení v rozmezí hodnot 0-5
        // double je dimenzováno na podstatně větší rozsah hodnot (8 B)
        // jaká je hodnota této vlastnosti, pokud ještě nikdo nepřidal žádné hodnocení?
            // defaultní hodnota je 0.0d, ta ale může nastat i pokud bude mít film pouze nulová hodnocení
            // nedefinovaný stav tedy odlišíme hodnotou null, proto je u datového typu otazník (dělá z něj "nullovatelný" typ)

        private List<double> dosavadniHodnoceni; // zde je pouze deklarace, nikoli inicializace tohoto listu
        // použijeme List nikoli pole, jelikož se počet hodnocení bude měnit

        /// <summary>
        /// Aktualizuje celkové hodnocení filmu o nové hodnocení.
        /// </summary>
        /// <param name="noveHodnoceni"></param>
        public void PridejHodnoceni(double noveHodnoceni)
        {
            // pokud ještě žádné hodnocení nebylo přidáno
            if (Hodnoceni == null)
            {
                Hodnoceni = noveHodnoceni;
            }
            else
            {
                // přidáme nové hodnocení
                dosavadniHodnoceni.Add(noveHodnoceni);

                // spočítáme průměr
                double suma = 0;
                foreach (double hodnoceni in dosavadniHodnoceni)
                {
                    suma += hodnoceni;
                }
                double prumer = suma / dosavadniHodnoceni.Count;

                // zapíšeme do veřejné vlastnosti Hodnocení
                Hodnoceni = prumer;
            }                
        }

        public string VypisDosavadniHodnoceni()
        {
            // pokud ještě žádné hodnocení nebylo přidáno
            if (Hodnoceni == null)
            {
                return "Žádné hodnocení nebylo doposud přidáno.";
            }
            else 
            {
                // uvnitř jiné třídy než Program nechceme pracovat s konzolí -> budeme pouze vracet string

                // StringBuilder - šetrnější způsob, jak měnit string
                StringBuilder sb = new StringBuilder();
                foreach (double hodnoceni in dosavadniHodnoceni)
                {
                    sb.Append(hodnoceni);
                    sb.Append("; ");
                }
                return sb.ToString();
            }
                
        }

        public override string ToString()
        {
            if (Hodnoceni == null)
                return $"{Nazev} ({RokVzniku}); {PrijmeniRezisera}, {JmenoRezisera[0]}): žádné hodnocení";


            double zaokrouhleneHodnoceni = Math.Round((double)Hodnoceni, 1);
                // explicitní konverze Hodnoceni na double z double?, protože může nabývat null hodnoty 

            return $"{Nazev} ({RokVzniku}); {PrijmeniRezisera}, {JmenoRezisera[0]}): {zaokrouhleneHodnoceni}\u2605";
            // string (textový řetězec) je jen pole znaků - proto [0]
            //\u2605 - speciální znak hvězdičky, dokáže ji vypsat font konzole např. DejaVu Sans Mono
        }
    }
 

}
