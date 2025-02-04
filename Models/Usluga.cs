using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Usluga
    {
        [Key]
        public int Id { get; set; } // Klucz g³ówny

        [Required(ErrorMessage = "Nazwa us³ugi jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa us³ugi mo¿e mieæ maksymalnie 100 znaków.")]
        public string Nazwa { get; set; } // Nazwa us³ugi

        [Required(ErrorMessage = "Cena us³ugi jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi byæ wiêksza od 0.")]
        public decimal Cena { get; set; } // Cena us³ugi

        // Relacja: Us³uga mo¿e byæ przypisana do wielu rezerwacji
        public ICollection<Rezerwacja> Rezerwacje { get; set; }
    }
}
