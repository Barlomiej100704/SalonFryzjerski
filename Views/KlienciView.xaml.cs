using System.Windows;
using SalonFryzjerski.ViewModels;

namespace SalonFryzjerski.Views
{
    public partial class KlientView : Window
    {
        public KlientView()
        {
            InitializeComponent();
            DataContext = new KlientViewModel();
        }
    }
}
