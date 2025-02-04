using System;
using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Models
{
    public class Rezerwacja
    {
        [Key]
        public int Id { get; set; } // Klucz g³ówny

        [Required]
        public int KlientId { get; set; } // Klucz obcy do Klient
        public Klient Klient { get; set; } // Nawigacja do Klient

        [Required]
        public int FryzjerId { get; set; } // Klucz obcy do Fryzjer
        public Fryzjer Fryzjer { get; set; } // Nawigacja do Fryzjer

        [Required]
        public int UslugaId { get; set; } // Klucz obcy do Usluga
        public Usluga Usluga { get; set; } // Nawigacja do Usluga

        [Required]
        public DateTime? DataRezerwacji { get; set; } // Data rezerwacji

        [Required]
        public TimeSpan CzasRezerwacji { get; set; } // Godzina rezerwacji
    }
}
