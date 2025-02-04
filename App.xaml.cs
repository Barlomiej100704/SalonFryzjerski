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
                    // Zastosowanie migracji i utworzenie bazy danych (je�li nie istnieje)
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� b��d podczas inicjalizacji bazy danych:\n{ex.Message}",
                                "B��d",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                Shutdown(); // Zako�cz aplikacj� w przypadku b��du
            }
        }
    }
}
