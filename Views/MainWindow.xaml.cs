
using System.Windows;

namespace SalonFryzjerski.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigateToKlienciView(object sender, RoutedEventArgs e)
        {
            new KlientView().Show();
        }

        private void NavigateToRezerwacjaView(object sender, RoutedEventArgs e)
        {
            new RezerwacjaView().Show();
        }

        private void NavigateToFryzjerView(object sender, RoutedEventArgs e)
        {
            new FryzjerView().Show();
        }

        private void NavigateToUslugaView(object sender, RoutedEventArgs e)
        {
            new UslugaView().Show();
        }

        private void NavigateToHarmonogramView(object sender, RoutedEventArgs e)
        {
            new HarmonogramView().Show();
        }
        private void NavigateToRaportKlientow(object sender, RoutedEventArgs e)
        {
            new RaportKlientowView().Show(); // Zainicjuj i poka¿ widok raportu klientów
        }
        private void NavigateToRaportRezerwacji(object sender, RoutedEventArgs e)
        {
            new RaportRezerwacjiView().Show(); // Zainicjuj i poka¿ widok raportu klientów
        }
    }
}
