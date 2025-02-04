using Microsoft.EntityFrameworkCore;
using SalonFryzjerski.Data;
using SalonFryzjerski.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalonFryzjerski.Services
{
    public class RezerwacjaService : IEntityService<Rezerwacja>
    {
        private readonly ApplicationDbContext _context;

        public RezerwacjaService()
        {
            _context = new ApplicationDbContext();
        }

        public List<Rezerwacja> LoadData()
        {
            // Ładowanie rezerwacji z powiązanymi encjami
            return _context.Rezerwacje
                .Include(r => r.Klient)
                .Include(r => r.Fryzjer)
                .Include(r => r.Usluga)
                .ToList();
        }

        public bool IsValid(Rezerwacja rezerwacja, out string errors)
        {
            errors = string.Empty;
            if (rezerwacja.KlientId <= 0 || rezerwacja.FryzjerId <= 0 || rezerwacja.UslugaId <= 0)
            {
                errors = "Klient, Fryzjer i Usługa są wymagane.";
                return false;
            }
            return true;
        }

        public bool SaveData(Rezerwacja rezerwacja)
        {
            // Upewnij się, że właściwości kluczy obcych są ustawione,
            // a referencje do obiektów nawigacyjnych usunięte, aby uniknąć konfliktów przy śledzeniu.
            if (rezerwacja.Klient != null)
            {
                rezerwacja.KlientId = rezerwacja.Klient.Id;
                rezerwacja.Klient = null;
            }
            if (rezerwacja.Fryzjer != null)
            {
                rezerwacja.FryzjerId = rezerwacja.Fryzjer.Id;
                rezerwacja.Fryzjer = null;
            }
            if (rezerwacja.Usluga != null)
            {
                rezerwacja.UslugaId = rezerwacja.Usluga.Id;
                rezerwacja.Usluga = null;
            }

            if (rezerwacja.Id == 0)
            {
                _context.Rezerwacje.Add(rezerwacja);
            }
            else
            {
                _context.Rezerwacje.Update(rezerwacja);
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteData(int id)
        {
            var rezerwacja = _context.Rezerwacje.Find(id);
            if (rezerwacja != null)
            {
                _context.Rezerwacje.Remove(rezerwacja);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
