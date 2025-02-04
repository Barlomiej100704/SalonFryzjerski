
using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class RezerwacjaView : Window
    {
        public RezerwacjaView()
        {
            InitializeComponent();
            DataContext = new RezerwacjaViewModel();
        }
    }
}
