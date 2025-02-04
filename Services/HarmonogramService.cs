using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalonFryzjerski.Data;
using SalonFryzjerski.Models;

namespace SalonFryzjerski.Services
{
    public class HarmonogramService : IEntityService<Harmonogram>
    {
        // Wczytywanie danych harmonogramów
        public List<Harmonogram> LoadData()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Harmonogramy
                    .Include(h => h.Fryzjer) // Za³aduj powi¹zanych fryzjerów
                    .ToList();
            }
        }

        // Wczytywanie listy fryzjerów
        public List<Fryzjer> LoadFryzjerzy()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Fryzjerzy.ToList(); // Pobierz wszystkich fryzjerów
            }
        }

        // Walidacja harmonogramu
        public bool IsValid(Harmonogram harmonogram, out string errors)
        {
            var context = new ValidationContext(harmonogram);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(harmonogram, context, validationResults, true);

            errors = string.Join("\n", validationResults.Select(v => v.ErrorMessage));
            return isValid;
        }

        // Zapis harmonogramu
        public bool SaveData(Harmonogram harmonogram)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // SprawdŸ istnienie fryzjera
                    var fryzjer = context.Fryzjerzy.Find(harmonogram.FryzjerId);
                    if (fryzjer == null)
                    {
                        throw new Exception("Wybrany fryzjer nie istnieje.");
                    }

                    harmonogram.Fryzjer = fryzjer;

                    // Dodaj lub zaktualizuj harmonogram
                    if (harmonogram.Id == 0)
                    {
                        context.Harmonogramy.Add(harmonogram);
                    }
                    else
                    {
                        context.Harmonogramy.Update(harmonogram);
                    }

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas zapisywania harmonogramu: {ex.Message}");
                return false;
            }
        }

        // Usuwanie harmonogramu
        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var harmonogram = context.Harmonogramy.Find(id);
                    if (harmonogram != null)
                    {
                        context.Harmonogramy.Remove(harmonogram);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas usuwania harmonogramu: {ex.Message}");
            }

            return false;
        }
    }
}
