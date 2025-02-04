using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Usluga
    {
        [Key]
        public int Id { get; set; } // Klucz g��wny

        [Required(ErrorMessage = "Nazwa us�ugi jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa us�ugi mo�e mie� maksymalnie 100 znak�w.")]
        public string Nazwa { get; set; } // Nazwa us�ugi

        [Required(ErrorMessage = "Cena us�ugi jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi by� wi�ksza od 0.")]
        public decimal Cena { get; set; } // Cena us�ugi

        // Relacja: Us�uga mo�e by� przypisana do wielu rezerwacji
        public ICollection<Rezerwacja> Rezerwacje { get; set; }
    }
}
