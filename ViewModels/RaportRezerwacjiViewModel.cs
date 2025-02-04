using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using System.Linq;
using SalonFryzjerski.Helpers;

namespace SalonFryzjerski.ViewModels
{
    public class RaportRezerwacjiViewModel : ViewModelBase
    {
        private readonly RezerwacjaService _rezerwacjaService;

        public ObservableCollection<Rezerwacja> Rezerwacje { get; set; }
        public ObservableCollection<Rezerwacja> WynikRezerwacji { get; set; }

        // Filtry
        public string KlientImieFiltr { get; set; }
        public string FryzjerImieFiltr { get; set; }
        public string UsługaNazwaFiltr { get; set; }

        public ICommand FiltrujCommand { get; }
        public ICommand ResetujCommand { get; }

        public RaportRezerwacjiViewModel()
        {
            _rezerwacjaService = new RezerwacjaService();
            Rezerwacje = new ObservableCollection<Rezerwacja>(_rezerwacjaService.LoadData());
            WynikRezerwacji = new ObservableCollection<Rezerwacja>(Rezerwacje);

            FiltrujCommand = new RelayCommand(Filtruj);
            ResetujCommand = new RelayCommand(Resetuj);
        }

        // Metoda do filtrowania
        private void Filtruj()
        {
            var wynik = Rezerwacje.Where(r =>
                (string.IsNullOrEmpty(KlientImieFiltr) || r.Klient.Imie.Contains(KlientImieFiltr)) &&
                (string.IsNullOrEmpty(FryzjerImieFiltr) || r.Fryzjer.Imie.Contains(FryzjerImieFiltr)) &&
                (string.IsNullOrEmpty(UsługaNazwaFiltr) || r.Usluga.Nazwa.Contains(UsługaNazwaFiltr))
            ).ToList();

            WynikRezerwacji.Clear();
            foreach (var rezerwacja in wynik)
            {
                WynikRezerwacji.Add(rezerwacja);
            }
        }

        // Metoda do resetowania filtrów
        private void Resetuj()
        {
            KlientImieFiltr = string.Empty;
            FryzjerImieFiltr = string.Empty;
            UsługaNazwaFiltr = string.Empty;

            WynikRezerwacji.Clear();
            foreach (var rezerwacja in Rezerwacje)
            {
                WynikRezerwacji.Add(rezerwacja);
            }
        }
    }
}
