using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SalonFryzjerski.Data;
using SalonFryzjerski.Models;

namespace SalonFryzjerski.Services
{
    public class KlientService : IEntityService<Klient>
    {
        private readonly ApplicationDbContext _context;

        public KlientService()
        {
            _context = new ApplicationDbContext();
        }

        // Wczytywanie danych z bazy
        public List<Klient> LoadData()
        {
            return _context.Klienci.ToList(); // Pobierz wszystkich klientów z bazy
        }

        // Walidacja danych klienta
        public bool IsValid(Klient klient, out string errors)
        {
            var context = new ValidationContext(klient);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(klient, context, validationResults, true);

            errors = string.Join("\n", validationResults.Select(v => v.ErrorMessage));
            return isValid;
        }

        // Dodawanie lub aktualizowanie klienta w bazie
        public bool SaveData(Klient klient)
        {
            if (klient.Id == 0)
            {
                _context.Klienci.Add(klient); // Dodaj nowego klienta
            }
            else
            {
                var existingKlient = _context.Klienci.Find(klient.Id);
                if (existingKlient != null)
                {
                    existingKlient.Imie = klient.Imie;
                    existingKlient.Nazwisko = klient.Nazwisko;
                    existingKlient.Telefon = klient.Telefon;
                }
            }

            _context.SaveChanges(); // Zapisz zmiany w bazie
            return true;
        }

        // Usuwanie klienta z bazy
        public bool DeleteData(int id)
        {
            var klient = _context.Klienci.Find(id);
            if (klient != null)
            {
                _context.Klienci.Remove(klient);
                _context.SaveChanges(); // Zapisz zmiany w bazie
                return true;
            }

            return false;
        }
    }
}
