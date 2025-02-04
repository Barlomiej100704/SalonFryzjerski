using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Klient
    {
        [Key]
        public int Id { get; set; } // Klucz g³ówny

        [Required(ErrorMessage = "Imiê jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imiê mo¿e mieæ maksymalnie 50 znaków.")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(50, ErrorMessage = "Nazwisko mo¿e mieæ maksymalnie 50 znaków.")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi sk³adaæ siê z 9 cyfr.")]
        public string Telefon { get; set; }

        // Relacja: Klient mo¿e mieæ wiele rezerwacji
        public ICollection<Rezerwacja> Rezerwacje { get; set; }
    }
}
