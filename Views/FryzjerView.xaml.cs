
using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class FryzjerView : Window
    {
        public FryzjerView()
        {
            InitializeComponent();
            DataContext = new FryzjerViewModel();
        }
    }
}
