using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SalonFryzjerski.Data;

namespace SalonFryzjerski
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Zastosowanie migracji i utworzenie bazy danych (jeœli nie istnieje)
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst¹pi³ b³¹d podczas inicjalizacji bazy danych:\n{ex.Message}",
                                "B³¹d",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                Shutdown(); // Zakoñcz aplikacjê w przypadku b³êdu
            }
        }
    }
}
