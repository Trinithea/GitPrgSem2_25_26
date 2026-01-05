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

namespace WPFUvodniHodina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Klik(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = "Ahoj";
            if (txtPozdrav.Text == "")
                MessageBox.Show("Vstupní pole je prázdné", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                ((Button)sender).Content = txtPozdrav.Text;
        }

        private void btnPozdrav_MouseEnter(object sender, MouseEventArgs e)
        {
            //Náhodné r,g,b
            Random rnd = new Random();
            byte r = (byte)rnd.Next(255);
            byte g = (byte)rnd.Next(255);
            byte b = (byte)rnd.Next(255);

            // barva tlačítka
            //btnPozdrav.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            byte diff = 40;
            btnPozdrav.Background = new LinearGradientBrush(
                Color.FromRgb(r, g, b), // start color
                Color.FromRgb((byte)Math.Max(r, r + diff), (byte)Math.Max(r, r + diff), (byte)Math.Max(r, r + diff)), // end color
                90); // angle
            
        }

       
    }
}