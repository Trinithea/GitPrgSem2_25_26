using Malovani;
using System.Drawing;
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

namespace MalovaniZHodiny
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

        

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"; ; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result.Value)
            {
                // Save document
                string filename = dlg.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RGBColorPicker rGBColorPicker = new RGBColorPicker();
            bool? success = rGBColorPicker.ShowDialog(); // musí si vybrat!

            if (rGBColorPicker.Success == true) {
                ColorButton.Background = new SolidColorBrush(rGBColorPicker.Color);
            }

        }
        /*
        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                // Získání aktuální pozice myši
                System.Windows.Point currentPoint = e.GetPosition(DrawingCanvas);

                // Vytvoření kruhu (Ellipse)
                Ellipse circle = new Ellipse
                {
                    Width = _circleRadius,
                    Height = _circleRadius,
                    Fill = new SolidColorBrush(_color) // Barva kruhu
                };

                // Nastavení pozice kruhu
                Canvas.SetLeft(circle, currentPoint.X - _circleRadius);
                Canvas.SetTop(circle, currentPoint.Y - _circleRadius);

                // Přidání kruhu na Canvas
                DrawingCanvas.Children.Add(circle);
            }
        }
        */
    }
}