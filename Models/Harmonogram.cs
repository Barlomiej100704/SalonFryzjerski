using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonFryzjerski.Models
{
    public class Harmonogram
    {
        [Key]
        public int Id { get; set; } // Klucz g³ówny

        [Required]
        public int FryzjerId { get; set; } // Klucz obcy wskazuj¹cy na fryzjera
        [ForeignKey("FryzjerId")]
        public Fryzjer Fryzjer { get; set; } // Nawigacja do encji Fryzjer

        [Required]
        public DateTime Data { get; set; } // Data harmonogramu

        [Required]
        public bool CzyDostepne { get; set; } // Czy termin jest dostêpny

        // Nowe w³aœciwoœci
        [Required]
        [StringLength(20)]
        public string DzienTygodnia { get; set; } // Dzieñ tygodnia (np. Poniedzia³ek)

        [Required]
        [StringLength(5)]
        public string GodzinaRozpoczecia { get; set; } // Godzina rozpoczêcia w formacie HH:mm

        [Required]
        [StringLength(5)]
        public string GodzinaZakonczenia { get; set; } // Godzina zakoñczenia w formacie HH:mm
    }
}
