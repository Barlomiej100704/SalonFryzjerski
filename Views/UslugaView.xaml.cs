using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class UslugaView : Window
    {
        public UslugaView()
        {
            InitializeComponent();
            DataContext = new UslugaViewModel();
        }
    }
}
