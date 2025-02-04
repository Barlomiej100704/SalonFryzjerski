using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class HarmonogramView : Window
    {
        public HarmonogramView()
        {
            InitializeComponent();
            DataContext = new HarmonogramViewModel();
        }
    }
}
