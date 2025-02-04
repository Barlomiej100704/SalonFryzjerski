using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SalonFryzjerski.Models;
using SalonFryzjerski.Services;
using SalonFryzjerski.Helpers;

namespace SalonFryzjerski.ViewModels
{
    public class KlientViewModel : ViewModelBase
    {
        private readonly KlientService _klientService;

        public ObservableCollection<Klient> Klienci { get; set; }

        private Klient _selectedKlient;
        public Klient SelectedKlient
        {
            get => _selectedKlient;
            set
            {
                _selectedKlient = value;
                OnPropertyChanged();
            }
        }

        private Klient _newKlient;
        public Klient NewKlient
        {
            get => _newKlient;
            set
            {
                _newKlient = value;
                OnPropertyChanged();
            }
        }

        public KlientViewModel()
        {
            _klientService = new KlientService();
            Klienci = new ObservableCollection<Klient>(_klientService.LoadData());
            NewKlient = new Klient(); // Inicjalizacja nowego klienta
        }

        public ICommand AddCommand => new RelayCommand(AddKlient);
        public ICommand DeleteCommand => new RelayCommand(DeleteKlient, CanDeleteKlient);

        private void AddKlient()
        {
            if (!string.IsNullOrWhiteSpace(NewKlient.Imie) &&
                !string.IsNullOrWhiteSpace(NewKlient.Nazwisko) &&
                !string.IsNullOrWhiteSpace(NewKlient.Telefon))
            {
                if (_klientService.IsValid(NewKlient, out var errors))
                {
                    _klientService.SaveData(NewKlient); // Zapis do bazy
                    Klienci.Add(NewKlient); // Dodanie do lokalnej listy
                    MessageBox.Show("Klient dodany.");
                    NewKlient = new Klient(); // Reset
                    OnPropertyChanged(nameof(NewKlient));
                }
                else
                {
                    MessageBox.Show(errors, "B³êdy walidacji");
                }
            }
            else
            {
                MessageBox.Show("Uzupe³nij wszystkie pola.");
            }
        }

        private bool CanDeleteKlient()
        {
            return SelectedKlient != null;
        }

        private void DeleteKlient()
        {
            if (SelectedKlient != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usun¹æ klienta: {SelectedKlient.Imie} {SelectedKlient.Nazwisko}?",
                    "Potwierdzenie usuniêcia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (_klientService.DeleteData(SelectedKlient.Id)) // Usuwanie z bazy
                    {
                        Klienci.Remove(SelectedKlient); // Usuwanie z lokalnej listy
                        MessageBox.Show("Klient zosta³ usuniêty.");
                    }
                    else
                    {
                        MessageBox.Show("Wyst¹pi³ b³¹d podczas usuwania klienta.");
                    }
                }
            }
        }
    }
}
