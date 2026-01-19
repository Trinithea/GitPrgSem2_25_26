using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBindingProject
{
    // Hlavní okno aplikace.
    // Implementujeme rozhraní INotifyPropertyChanged,
    // aby WPF vědělo, že se některé vlastnosti mohou měnit
    // a má je znovu vykreslit v uživatelském rozhraní.
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            // DataContext říká WPF:
            // "Hledej data pro databinding v této třídě (MainWindow)."
            DataContext = this;

            // Načte rozhraní definované v XAML souboru.
            InitializeComponent();
        }

        // Soukromá proměnná, ve které si skutečně ukládáme text.
        // Uživatelské rozhraní na ni ale přímo nesahá.
        private string boundText;        

        // Veřejná vlastnost, na kterou se váže UI (např. TextBox, Label…)
        public string BoundText
        {
            get { return boundText; }
            set
            {
                // Uložíme novou hodnotu
                boundText = value;

                // Oznámíme WPF:
                // "Hodnota se změnila, aktualizuj uživatelské rozhraní."
                OnPropertyChanged();
            }
        }
              

        // Událost, kterou vyžaduje rozhraní INotifyPropertyChanged.
        // WPF se na ni "přihlásí" a čeká, až oznámíme změnu dat.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Pomocná metoda, která vyvolá událost PropertyChanged.
        // [CallerMemberName] automaticky doplní název vlastnosti,
        // ze které tuto metodu voláme (zde: "BoundText").
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Pokud je na událost někdo přihlášen (např. WPF),
            // pošleme mu informaci, která vlastnost se změnila.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // PropertyChanged - je takový zvonek
            // ten ? říká, že budem zvonit, jen pokud je někdo doma (tedy někdo PropertyChanged poslouchá (např. WPF), není to null)
            // Invoke(...) - zavolá všechny metody, které jsou na PropertyChanged přihlášené (typicky celý ten WPF databindingový systém).
            // this - "Já (MainWindow) hlásím, že se u mě něco změnilo."
            // new PropertyChangedEventArgs(propertyName) - balíček informací, který posíláme (např. jméno proměnné BoundText)

            // Shrnutí: „Pokud mě někdo poslouchá, zavolej ho a řekni mu, že se v tomto objektu změnila vlastnost s názvem propertyName.“
            // Shrnutí 2: „Hej WPF, změnila se tahle vlastnost, překresli si UI.“
        }


        // Metoda, která se zavolá po kliknutí na tlačítko (z XAML).
        // Jen pro ukázku příkladu změny z kódu.
        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            // Změníme hodnotu vlastnosti.
            // Pokud je správně nastaven databinding,
            // UI se aktualizuje automaticky.
            BoundText = "set from code";
        }
    }
}