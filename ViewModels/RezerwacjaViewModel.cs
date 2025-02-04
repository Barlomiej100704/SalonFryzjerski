using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using SalonFryzjerski.Helpers;

namespace SalonFryzjerski.ViewModels
{
    public class RezerwacjaViewModel : ViewModelBase
    {
        private readonly RezerwacjaService _rezerwacjaService;
        private readonly KlientService _klientService;
        private readonly FryzjerService _fryzjerService;
        private readonly UslugaService _uslugaService;

        public ObservableCollection<Rezerwacja> Rezerwacje { get; set; }
        public ObservableCollection<Klient> Klienci { get; set; }
        public ObservableCollection<Fryzjer> Fryzjerzy { get; set; }
        public ObservableCollection<Usluga> Uslugi { get; set; }
        public ObservableCollection<string> ListaGodzin { get; set; }

        private Rezerwacja _selectedRezerwacja;
        public Rezerwacja SelectedRezerwacja
        {
            get => _selectedRezerwacja;
            set
            {
                _selectedRezerwacja = value;
                OnPropertyChanged();
            }
        }

        private Rezerwacja _newRezerwacja;
        public Rezerwacja NewRezerwacja
        {
            get
            {
                if (_newRezerwacja == null)
                {
                    ResetNewRezerwacja();
                }
                return _newRezerwacja;
            }
            set
            {
                _newRezerwacja = value;
                OnPropertyChanged();
            }
        }

        private string _selectedGodzina;
        public string SelectedGodzina
        {
            get => _selectedGodzina;
            set
            {
                _selectedGodzina = value;
                if (TimeSpan.TryParse(_selectedGodzina, out var czas))
                {
                    // Ustawiamy czas rezerwacji w obiekcie NewRezerwacja
                    NewRezerwacja.CzasRezerwacji = czas;
                }
                OnPropertyChanged();
            }
        }

        public RezerwacjaViewModel()
        {
            _rezerwacjaService = new RezerwacjaService();
            _klientService = new KlientService();
            _fryzjerService = new FryzjerService();
            _uslugaService = new UslugaService();

            Rezerwacje = new ObservableCollection<Rezerwacja>(_rezerwacjaService.LoadData());
            Klienci = new ObservableCollection<Klient>(_klientService.LoadData());
            Fryzjerzy = new ObservableCollection<Fryzjer>(_fryzjerService.LoadData());
            Uslugi = new ObservableCollection<Usluga>(_uslugaService.LoadData());

            ListaGodzin = new ObservableCollection<string>
            {
                "08:00", "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00"
            };

            // Inicjalizacja nowej rezerwacji
            ResetNewRezerwacja();
        }

        public ICommand AddCommand => new RelayCommand(AddRezerwacja);
        public ICommand DeleteCommand => new RelayCommand(DeleteRezerwacja, CanDeleteRezerwacja);

        private void AddRezerwacja()
        {
            if (NewRezerwacja.KlientId <= 0)
            {
                MessageBox.Show("Wybierz klienta.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewRezerwacja.FryzjerId <= 0)
            {
                MessageBox.Show("Wybierz fryzjera.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewRezerwacja.UslugaId <= 0)
            {
                MessageBox.Show("Wybierz usługę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewRezerwacja.DataRezerwacji == null)
            {
                MessageBox.Show("Wybierz datę rezerwacji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ustawienie pełnej daty rezerwacji (data + wybrana godzina)
            NewRezerwacja.DataRezerwacji = NewRezerwacja.DataRezerwacji.Value.Date.Add(NewRezerwacja.CzasRezerwacji);

            // Sprawdzenie konfliktu: czy dla wybranego fryzjera o tym samym terminie już istnieje rezerwacja
            bool conflict = Rezerwacje.Any(r => r.FryzjerId == NewRezerwacja.FryzjerId &&
                                                 r.DataRezerwacji == NewRezerwacja.DataRezerwacji);

            if (conflict)
            {
                MessageBox.Show("Wybrany fryzjer ma już wizytę o tej godzinie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_rezerwacjaService.SaveData(NewRezerwacja))
            {
                // Po zapisie, ponieważ w SaveData mogły zostać wyzerowane właściwości nawigacyjne,
                // przypisujemy je z naszych lokalnych kolekcji, aby widok pokazywał pełne dane.
                NewRezerwacja.Klient = Klienci.FirstOrDefault(k => k.Id == NewRezerwacja.KlientId);
                NewRezerwacja.Fryzjer = Fryzjerzy.FirstOrDefault(f => f.Id == NewRezerwacja.FryzjerId);
                NewRezerwacja.Usluga = Uslugi.FirstOrDefault(u => u.Id == NewRezerwacja.UslugaId);

                Rezerwacje.Add(NewRezerwacja);
                MessageBox.Show("Rezerwacja została dodana.");
                ResetNewRezerwacja();
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas zapisywania rezerwacji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetNewRezerwacja()
        {
            // Jeśli użytkownik wcześniej wybrał godzinę, zachowujemy ją;
            // w przeciwnym razie, używamy pierwszej pozycji z listy (lub "08:00" jeśli lista jest pusta)
            string defaultGodzina = !string.IsNullOrEmpty(_selectedGodzina)
                ? _selectedGodzina
                : (ListaGodzin.Count > 0 ? ListaGodzin[0] : "08:00");

            // Nie nadpisujemy _selectedGodzina, dzięki czemu zachowamy aktualny wybór użytkownika

            _newRezerwacja = new Rezerwacja
            {
                Klient = Klienci.Count > 0 ? Klienci[0] : null,
                Fryzjer = Fryzjerzy.Count > 0 ? Fryzjerzy[0] : null,
                Usluga = Uslugi.Count > 0 ? Uslugi[0] : null,
                DataRezerwacji = null,
                CzasRezerwacji = TimeSpan.Parse(defaultGodzina)
            };
            OnPropertyChanged(nameof(NewRezerwacja));
        }

        private bool CanDeleteRezerwacja()
        {
            return SelectedRezerwacja != null;
        }

        private void DeleteRezerwacja()
        {
            if (SelectedRezerwacja != null)
            {
                if (_rezerwacjaService.DeleteData(SelectedRezerwacja.Id))
                {
                    Rezerwacje.Remove(SelectedRezerwacja);
                    MessageBox.Show("Rezerwacja została usunięta.");
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas usuwania rezerwacji.");
                }
            }
        }
    }
}
