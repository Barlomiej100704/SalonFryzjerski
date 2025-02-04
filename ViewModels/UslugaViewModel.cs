using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using SalonFryzjerski.Helpers;

namespace SalonFryzjerski.ViewModels
{
    public class UslugaViewModel : ViewModelBase
    {
        private readonly UslugaService _uslugaService;

        public ObservableCollection<Usluga> Uslugi { get; set; }

        private Usluga _selectedUsluga;
        public Usluga SelectedUsluga
        {
            get => _selectedUsluga;
            set
            {
                _selectedUsluga = value;
                OnPropertyChanged();
            }
        }

        private Usluga _newUsluga;
        public Usluga NewUsluga
        {
            get => _newUsluga;
            set
            {
                _newUsluga = value;
                OnPropertyChanged();
            }
        }

        public UslugaViewModel()
        {
            _uslugaService = new UslugaService();
            Uslugi = new ObservableCollection<Usluga>(_uslugaService.LoadData());
            NewUsluga = new Usluga(); // Inicjalizacja nowej us�ugi
        }

        public ICommand AddCommand => new RelayCommand(AddUsluga);
        public ICommand DeleteCommand => new RelayCommand(DeleteUsluga, CanDeleteUsluga);

        private void AddUsluga()
        {
            if (!string.IsNullOrWhiteSpace(NewUsluga.Nazwa) && NewUsluga.Cena > 0)
            {
                if (_uslugaService.IsValid(NewUsluga, out var errors))
                {
                    _uslugaService.SaveData(NewUsluga); // Zapis do bazy danych
                    Uslugi.Add(NewUsluga); // Dodanie do lokalnej listy
                    MessageBox.Show("Us�uga dodana.");
                    NewUsluga = new Usluga(); // Reset
                    OnPropertyChanged(nameof(NewUsluga));
                }
                else
                {
                    MessageBox.Show(errors, "B��dy walidacji");
                }
            }
            else
            {
                MessageBox.Show("Uzupe�nij wszystkie pola.");
            }
        }

        private bool CanDeleteUsluga()
        {
            return SelectedUsluga != null;
        }

        private void DeleteUsluga()
        {
            if (SelectedUsluga != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usun�� us�ug�: {SelectedUsluga.Nazwa}?",
                    "Potwierdzenie usuni�cia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (_uslugaService.DeleteData(SelectedUsluga.Id)) // Usuni�cie z bazy danych
                    {
                        Uslugi.Remove(SelectedUsluga); // Usuni�cie z lokalnej listy
                        MessageBox.Show("Us�uga zosta�a usuni�ta.");
                    }
                    else
                    {
                        MessageBox.Show("Wyst�pi� b��d podczas usuwania us�ugi.");
                    }
                }
            }
        }
    }
}
