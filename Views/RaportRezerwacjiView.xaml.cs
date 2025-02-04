using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class RaportRezerwacjiView : Window
    {
        public RaportRezerwacjiView()
        {
            InitializeComponent();
            DataContext = new RaportRezerwacjiViewModel(); // Przypisanie ViewModel
        }
    }
}
