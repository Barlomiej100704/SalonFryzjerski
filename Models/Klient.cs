using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Klient
    {
        [Key]
        public int Id { get; set; } // Klucz g��wny

        [Required(ErrorMessage = "Imi� jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imi� mo�e mie� maksymalnie 50 znak�w.")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(50, ErrorMessage = "Nazwisko mo�e mie� maksymalnie 50 znak�w.")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi sk�ada� si� z 9 cyfr.")]
        public string Telefon { get; set; }

        // Relacja: Klient mo�e mie� wiele rezerwacji
        public ICollection<Rezerwacja> Rezerwacje { get; set; }
    }
}
