using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Fryzjer
    {
        public int Id { get; set; } // Klucz g³ówny

        [Required(ErrorMessage = "Imiê jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imiê mo¿e mieæ maksymalnie 50 znaków.")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Specjalizacja jest wymagana.")]
        [StringLength(100, ErrorMessage = "Specjalizacja mo¿e mieæ maksymalnie 100 znaków.")]
        public string Specjalizacja { get; set; }

        // Relacja: Fryzjer ma wiele harmonogramów
        public ICollection<Harmonogram> Harmonogramy { get; set; }

        // Relacja: Fryzjer mo¿e mieæ wiele rezerwacji
        public ICollection<Rezerwacja> Rezerwacje { get; set; }
    }
}
