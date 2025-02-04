using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SalonFryzjerski.Data;
using SalonFryzjerski.Models;

namespace SalonFryzjerski.Services
{
    public class FryzjerService : IEntityService<Fryzjer>
    {
        private readonly ApplicationDbContext _context;

        public FryzjerService()
        {
            _context = new ApplicationDbContext();
        }

        // Wczytywanie danych z bazy
        public List<Fryzjer> LoadData()
        {
            return _context.Fryzjerzy.ToList(); // Pobierz wszystkich fryzjerów z bazy
        }

        // Walidacja danych fryzjera
        public bool IsValid(Fryzjer fryzjer, out string errors)
        {
            var context = new ValidationContext(fryzjer);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(fryzjer, context, validationResults, true);

            errors = string.Join("\n", validationResults.Select(v => v.ErrorMessage));
            return isValid;
        }

        // Dodawanie lub aktualizowanie fryzjera w bazie
        public bool SaveData(Fryzjer fryzjer)
        {
            if (fryzjer.Id == 0)
            {
                _context.Fryzjerzy.Add(fryzjer); // Dodaj nowego fryzjera
            }
            else
            {
                var existingFryzjer = _context.Fryzjerzy.Find(fryzjer.Id);
                if (existingFryzjer != null)
                {
                    existingFryzjer.Imie = fryzjer.Imie;
                    existingFryzjer.Specjalizacja = fryzjer.Specjalizacja;
                }
            }

            _context.SaveChanges(); // Zapisz zmiany w bazie
            return true;
        }

        // Usuwanie fryzjera z bazy
        public bool DeleteData(int id)
        {
            var fryzjer = _context.Fryzjerzy.Find(id);
            if (fryzjer != null)
            {
                _context.Fryzjerzy.Remove(fryzjer);
                _context.SaveChanges(); // Zapisz zmiany w bazie
                return true;
            }

            return false;
        }
    }
}
