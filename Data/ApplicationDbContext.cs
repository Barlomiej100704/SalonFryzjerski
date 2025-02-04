using Microsoft.EntityFrameworkCore;
using SalonFryzjerski.Models;
using System;

namespace SalonFryzjerski.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Fryzjer> Fryzjerzy { get; set; }
        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        public DbSet<Usluga> Uslugi { get; set; }
        public DbSet<Harmonogram> Harmonogramy { get; set; }
        public DbSet<Klient> Klienci { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SalonFryzjerski.db")
                  .EnableSensitiveDataLogging() // Szczegółowe logowanie
                  .LogTo(Console.WriteLine); // Logowanie zapytań SQL
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja między Usluga a Rezerwacja
            modelBuilder.Entity<Usluga>()
                .HasMany(u => u.Rezerwacje) // Usługa może być powiązana z wieloma rezerwacjami
                .WithOne(r => r.Usluga) // Rezerwacja ma jedną usługę
                .HasForeignKey(r => r.UslugaId) // Klucz obcy w Rezerwacja
                .OnDelete(DeleteBehavior.Restrict); // Zachowanie przy usuwaniu

            // Relacja między Harmonogram a Fryzjer
            modelBuilder.Entity<Harmonogram>()
                .HasOne(h => h.Fryzjer)
                .WithMany(f => f.Harmonogramy)
                .HasForeignKey(h => h.FryzjerId);

            // Relacja między Rezerwacja a Klient
            modelBuilder.Entity<Rezerwacja>()
                .HasOne(r => r.Klient)
                .WithMany(k => k.Rezerwacje)
                .HasForeignKey(r => r.KlientId);

            // Relacja między Rezerwacja a Fryzjer
            modelBuilder.Entity<Rezerwacja>()
                .HasOne(r => r.Fryzjer)
                .WithMany(f => f.Rezerwacje)
                .HasForeignKey(r => r.FryzjerId);

            // Mapowanie właściwości DataRezerwacji i CzasRezerwacji
            modelBuilder.Entity<Rezerwacja>()
                .Property(r => r.DataRezerwacji)
                .HasColumnType("TEXT"); // SQLite przechowuje DateTime jako TEXT

            modelBuilder.Entity<Rezerwacja>()
                .Property(r => r.CzasRezerwacji)
                .HasConversion(
                    v => v.ToString(), // TimeSpan -> String
                    v => TimeSpan.Parse(v)) // String -> TimeSpan
                .HasColumnType("TEXT"); // SQLite przechowuje TimeSpan jako TEXT

            // Dodanie początkowych danych dla Fryzjerzy
            modelBuilder.Entity<Fryzjer>().HasData(
                new Fryzjer { Id = 1, Imie = "Katarzyna", Specjalizacja = "Strzyżenie damskie" },
                new Fryzjer { Id = 2, Imie = "Tomasz", Specjalizacja = "Strzyżenie męskie" }
            );

            // Dodanie początkowych danych dla Uslugi
            modelBuilder.Entity<Usluga>().HasData(
                new Usluga { Id = 1, Nazwa = "Strzyżenie", Cena = 50 },
                new Usluga { Id = 2, Nazwa = "Farbowanie", Cena = 150 },
                new Usluga { Id = 3, Nazwa = "Stylizacja", Cena = 100 }
            );

            // Dodanie początkowych danych dla Klienci
            modelBuilder.Entity<Klient>().HasData(
                new Klient { Id = 1, Imie = "Anna", Nazwisko = "Kowalska", Telefon = "123456789" },
                new Klient { Id = 2, Imie = "Jan", Nazwisko = "Nowak", Telefon = "987654321" }
            );

            // Dodanie początkowych danych dla Harmonogramy
            modelBuilder.Entity<Harmonogram>().HasData(
                new Harmonogram
                {
                    Id = 1,
                    FryzjerId = 1,
                    DzienTygodnia = "Poniedziałek",
                    GodzinaRozpoczecia = "08:00",
                    GodzinaZakonczenia = "16:00",
                    CzyDostepne = true
                },
                new Harmonogram
                {
                    Id = 2,
                    FryzjerId = 2,
                    DzienTygodnia = "Wtorek",
                    GodzinaRozpoczecia = "09:00",
                    GodzinaZakonczenia = "17:00",
                    CzyDostepne = true
                }
            );

            // Dodanie początkowych danych dla Rezerwacje
            modelBuilder.Entity<Rezerwacja>().HasData(
                new Rezerwacja
                {
                    Id = 1,
                    KlientId = 1,
                    FryzjerId = 1,
                    UslugaId = 1,
                    DataRezerwacji = new DateTime(2023, 10, 10, 10, 0, 0),
                    CzasRezerwacji = new TimeSpan(10, 0, 0)
                },
                new Rezerwacja
                {
                    Id = 2,
                    KlientId = 2,
                    FryzjerId = 2,
                    UslugaId = 2,
                    DataRezerwacji = new DateTime(2023, 10, 11, 12, 0, 0),
                    CzasRezerwacji = new TimeSpan(12, 0, 0)
                }
            );
        }
    }
}
