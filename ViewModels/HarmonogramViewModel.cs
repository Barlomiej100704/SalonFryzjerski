using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using SalonFryzjerski.Helpers;
using System;

namespace SalonFryzjerski.ViewModels
{
    public class HarmonogramViewModel : ViewModelBase
    {
        private readonly HarmonogramService _harmonogramService;

        public ObservableCollection<Harmonogram> Harmonogramy { get; set; }

        private Harmonogram _selectedHarmonogram;
        public Harmonogram SelectedHarmonogram
        {
            get => _selectedHarmonogram;
            set
            {
                _selectedHarmonogram = value;
                OnPropertyChanged();
            }
        }

        private Harmonogram _newHarmonogram;
        public Harmonogram NewHarmonogram
        {
            get => _newHarmonogram;
            set
            {
                _newHarmonogram = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> DniTygodnia { get; } = new ObservableCollection<string>
        {
            "Poniedzia³ek", "Wtorek", "Œroda", "Czwartek", "Pi¹tek", "Sobota", "Niedziela"
        };
        public ObservableCollection<Fryzjer> Fryzjerzy { get; set; } // Lista fryzjerów

        public HarmonogramViewModel()
        {
            _harmonogramService = new HarmonogramService();
            Harmonogramy = new ObservableCollection<Harmonogram>(_harmonogramService.LoadData());
            Fryzjerzy = new ObservableCollection<Fryzjer>(_harmonogramService.LoadFryzjerzy());
            ResetNewHarmonogram();
        }

        public ICommand AddCommand => new RelayCommand(AddHarmonogram);
        public ICommand DeleteCommand => new RelayCommand(DeleteHarmonogram, CanDeleteHarmonogram);

        private void AddHarmonogram()
        {
            // Sprawdzenie, czy wszystkie wymagane pola s¹ wype³nione
            if (NewHarmonogram.FryzjerId <= 0)
            {
                MessageBox.Show("Wybierz fryzjera.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewHarmonogram.DzienTygodnia))
            {
                MessageBox.Show("Wybierz dzieñ tygodnia.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewHarmonogram.GodzinaRozpoczecia))
            {
                MessageBox.Show("Podaj godzinê rozpoczêcia.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewHarmonogram.GodzinaZakonczenia))
            {
                MessageBox.Show("Podaj godzinê zakoñczenia.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Zapis harmonogramu
            if (_harmonogramService.SaveData(NewHarmonogram))
            {
                Harmonogramy.Add(NewHarmonogram); // Dodanie do listy
                MessageBox.Show("Harmonogram dodany.");
                ResetNewHarmonogram(); // Reset formularza
            }
            else
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas zapisywania harmonogramu.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanDeleteHarmonogram()
        {
            return SelectedHarmonogram != null;
        }

        private void DeleteHarmonogram()
        {
            if (SelectedHarmonogram != null)
            {
                try
                {
                    if (MessageBox.Show($"Czy na pewno chcesz usun¹æ harmonogram dla dnia: {SelectedHarmonogram.DzienTygodnia}?",
                        "Potwierdzenie usuniêcia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (_harmonogramService.DeleteData(SelectedHarmonogram.Id))
                        {
                            Harmonogramy.Remove(SelectedHarmonogram); // Usuñ z listy
                            MessageBox.Show("Harmonogram zosta³ usuniêty.");
                        }
                        else
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas usuwania harmonogramu.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"B³¹d podczas usuwania harmonogramu: {ex.Message}", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ResetNewHarmonogram()
        {
            NewHarmonogram = new Harmonogram
            {
                FryzjerId = 0,
                DzienTygodnia = "",
                GodzinaRozpoczecia = "",
                GodzinaZakonczenia = ""
            };
        }
    }
}
