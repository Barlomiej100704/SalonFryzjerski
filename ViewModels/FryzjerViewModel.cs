using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using SalonFryzjerski.Helpers;

namespace SalonFryzjerski.ViewModels
{
    public class FryzjerViewModel : ViewModelBase
    {
        private readonly FryzjerService _fryzjerService;

        public ObservableCollection<Fryzjer> Fryzjerzy { get; set; }

        private Fryzjer _selectedFryzjer;
        public Fryzjer SelectedFryzjer
        {
            get => _selectedFryzjer;
            set
            {
                _selectedFryzjer = value;
                OnPropertyChanged();
            }
        }

        private Fryzjer _newFryzjer;
        public Fryzjer NewFryzjer
        {
            get => _newFryzjer;
            set
            {
                _newFryzjer = value;
                OnPropertyChanged();
            }
        }

        public FryzjerViewModel()
        {
            _fryzjerService = new FryzjerService();
            Fryzjerzy = new ObservableCollection<Fryzjer>(_fryzjerService.LoadData());
            NewFryzjer = new Fryzjer();
        }

        // Komenda dodawania fryzjera
        public ICommand AddCommand => new RelayCommand(AddFryzjer);

        private void AddFryzjer()
        {
            if (_fryzjerService.IsValid(NewFryzjer, out var errors))
            {
                if (NewFryzjer.Id == 0) // Nowy fryzjer
                {
                    _fryzjerService.SaveData(NewFryzjer);
                    Fryzjerzy.Add(NewFryzjer); // Dodanie do listy
                    MessageBox.Show("Dodano fryzjera.");
                }
                else
                {
                    MessageBox.Show("Fryzjer o podanym ID ju¿ istnieje. Nie mo¿na dodaæ duplikatu.");
                }

                NewFryzjer = new Fryzjer();
                OnPropertyChanged(nameof(NewFryzjer));
            }
            else
            {
                MessageBox.Show(errors, "B³êdy walidacji");
            }
        }

        // Komenda usuwania fryzjera
        public ICommand DeleteCommand => new RelayCommand(DeleteFryzjer, CanDeleteFryzjer);

        private bool CanDeleteFryzjer()
        {
            return SelectedFryzjer != null; // Mo¿na usun¹æ tylko, gdy zaznaczony jest fryzjer
        }

        private void DeleteFryzjer()
        {
            if (SelectedFryzjer != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usun¹æ fryzjera: {SelectedFryzjer.Imie}?",
                    "Potwierdzenie usuniêcia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (_fryzjerService.DeleteData(SelectedFryzjer.Id))
                    {
                        Fryzjerzy.Remove(SelectedFryzjer); // Usuñ z listy
                        MessageBox.Show("Fryzjer zosta³ usuniêty.");
                    }
                    else
                    {
                        MessageBox.Show("Wyst¹pi³ b³¹d podczas usuwania fryzjera.");
                    }
                }
            }
        }
    }
}
