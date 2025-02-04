using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using System.Linq;
using SalonFryzjerski.Helpers;

namespace SalonFryzjerski.ViewModels
{
    public class RaportKlientowViewModel : ViewModelBase
    {
        private readonly KlientService _klientService;

        public ObservableCollection<Klient> Klienci { get; set; }
        public ObservableCollection<Klient> WynikKlientow { get; set; }

        // Filtry
        public string ImieFiltr { get; set; }
        public string NazwiskoFiltr { get; set; }
        public string TelefonFiltr { get; set; }

        public ICommand FiltrujCommand { get; }
        public ICommand ResetujCommand { get; }

        public RaportKlientowViewModel()
        {
            _klientService = new KlientService();
            Klienci = new ObservableCollection<Klient>(_klientService.LoadData());
            WynikKlientow = new ObservableCollection<Klient>(Klienci);

            FiltrujCommand = new RelayCommand(Filtruj);
            ResetujCommand = new RelayCommand(Resetuj);
        }

        // Metoda do filtrowania
        private void Filtruj()
        {
            var wynik = Klienci.Where(k =>
                (string.IsNullOrEmpty(ImieFiltr) || k.Imie.Contains(ImieFiltr)) &&
                (string.IsNullOrEmpty(NazwiskoFiltr) || k.Nazwisko.Contains(NazwiskoFiltr)) &&
                (string.IsNullOrEmpty(TelefonFiltr) || k.Telefon.Contains(TelefonFiltr))
            ).ToList();

            WynikKlientow.Clear();
            foreach (var klient in wynik)
            {
                WynikKlientow.Add(klient);
            }
        }

        // Metoda do resetowania filtrów
        private void Resetuj()
        {
            ImieFiltr = string.Empty;
            NazwiskoFiltr = string.Empty;
            TelefonFiltr = string.Empty;

            WynikKlientow.Clear();
            foreach (var klient in Klienci)
            {
                WynikKlientow.Add(klient);
            }
        }
    }
}
