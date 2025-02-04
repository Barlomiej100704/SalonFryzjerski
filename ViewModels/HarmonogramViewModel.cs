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
            "Poniedzia�ek", "Wtorek", "�roda", "Czwartek", "Pi�tek", "Sobota", "Niedziela"
        };
        public ObservableCollection<Fryzjer> Fryzjerzy { get; set; } // Lista fryzjer�w

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
            // Sprawdzenie, czy wszystkie wymagane pola s� wype�nione
            if (NewHarmonogram.FryzjerId <= 0)
            {
                MessageBox.Show("Wybierz fryzjera.", "B��d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewHarmonogram.DzienTygodnia))
            {
                MessageBox.Show("Wybierz dzie� tygodnia.", "B��d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewHarmonogram.GodzinaRozpoczecia))
            {
                MessageBox.Show("Podaj godzin� rozpocz�cia.", "B��d", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewHarmonogram.GodzinaZakonczenia))
            {
                MessageBox.Show("Podaj godzin� zako�czenia.", "B��d", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show("Wyst�pi� b��d podczas zapisywania harmonogramu.", "B��d", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    if (MessageBox.Show($"Czy na pewno chcesz usun�� harmonogram dla dnia: {SelectedHarmonogram.DzienTygodnia}?",
                        "Potwierdzenie usuni�cia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (_harmonogramService.DeleteData(SelectedHarmonogram.Id))
                        {
                            Harmonogramy.Remove(SelectedHarmonogram); // Usu� z listy
                            MessageBox.Show("Harmonogram zosta� usuni�ty.");
                        }
                        else
                        {
                            MessageBox.Show("Wyst�pi� b��d podczas usuwania harmonogramu.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"B��d podczas usuwania harmonogramu: {ex.Message}", "B��d", MessageBoxButton.OK, MessageBoxImage.Error);
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
