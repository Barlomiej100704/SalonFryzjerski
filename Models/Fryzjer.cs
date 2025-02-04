using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Fryzjer
    {
        public int Id { get; set; } // Klucz g��wny

        [Required(ErrorMessage = "Imi� jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imi� mo�e mie� maksymalnie 50 znak�w.")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Specjalizacja jest wymagana.")]
        [StringLength(100, ErrorMessage = "Specjalizacja mo�e mie� maksymalnie 100 znak�w.")]
        public string Specjalizacja { get; set; }

        // Relacja: Fryzjer ma wiele harmonogram�w
        public ICollection<Harmonogram> Harmonogramy { get; set; }

        // Relacja: Fryzjer mo�e mie� wiele rezerwacji
        public ICollection<Rezerwacja> Rezerwacje { get; set; }
    }
}
