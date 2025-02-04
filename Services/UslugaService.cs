using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SalonFryzjerski.Data;
using SalonFryzjerski.Models;

namespace SalonFryzjerski.Services
{
    public class UslugaService : IEntityService<Usluga>
    {
        private readonly ApplicationDbContext _context;

        public UslugaService()
        {
            _context = new ApplicationDbContext();
        }

        // Wczytywanie danych z bazy
        public List<Usluga> LoadData()
        {
            return _context.Uslugi.ToList(); // Pobierz wszystkie usługi z bazy
        }

        // Walidacja danych usługi
        public bool IsValid(Usluga usluga, out string errors)
        {
            var context = new ValidationContext(usluga);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(usluga, context, validationResults, true);

            errors = string.Join("\n", validationResults.Select(v => v.ErrorMessage));
            return isValid;
        }

        // Dodawanie lub aktualizowanie usługi w bazie
        public bool SaveData(Usluga usluga)
        {
            if (usluga.Id == 0)
            {
                // Dodaj nową usługę
                _context.Uslugi.Add(usluga);
            }
            else
            {
                // Aktualizacja istniejącej usługi
                var existingUsluga = _context.Uslugi.Find(usluga.Id);
                if (existingUsluga != null)
                {
                    existingUsluga.Nazwa = usluga.Nazwa;
                    existingUsluga.Cena = usluga.Cena;
                }
            }

            _context.SaveChanges(); // Zapisz zmiany w bazie
            return true;
        }

        // Usuwanie usługi z bazy
        public bool DeleteData(int id)
        {
            var usluga = _context.Uslugi.Find(id);
            if (usluga != null)
            {
                _context.Uslugi.Remove(usluga);
                _context.SaveChanges(); // Zapisz zmiany w bazie
                return true;
            }

            return false;
        }
    }
}
