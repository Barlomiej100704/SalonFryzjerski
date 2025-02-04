using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class RaportKlientowView : Window
    {
        public RaportKlientowView()
        {
            InitializeComponent();
            DataContext = new RaportKlientowViewModel(); // Przypisanie ViewModel
        }
    }
}
