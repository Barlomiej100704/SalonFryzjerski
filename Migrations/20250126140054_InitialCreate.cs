using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonFryzjerski.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fryzjerzy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Specjalizacja = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fryzjerzy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uslugi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uslugi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Harmonogramy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FryzjerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CzyDostepne = table.Column<bool>(type: "INTEGER", nullable: false),
                    DzienTygodnia = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    GodzinaRozpoczecia = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    GodzinaZakonczenia = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harmonogramy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Harmonogramy_Fryzjerzy_FryzjerId",
                        column: x => x.FryzjerId,
                        principalTable: "Fryzjerzy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezerwacje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KlientId = table.Column<int>(type: "INTEGER", nullable: false),
                    FryzjerId = table.Column<int>(type: "INTEGER", nullable: false),
                    UslugaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataRezerwacji = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CzasRezerwacji = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezerwacje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezerwacje_Fryzjerzy_FryzjerId",
                        column: x => x.FryzjerId,
                        principalTable: "Fryzjerzy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezerwacje_Klienci_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Klienci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezerwacje_Uslugi_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Uslugi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Fryzjerzy",
                columns: new[] { "Id", "Imie", "Specjalizacja" },
                values: new object[] { 1, "Katarzyna", "Strzyżenie damskie" });

            migrationBuilder.InsertData(
                table: "Fryzjerzy",
                columns: new[] { "Id", "Imie", "Specjalizacja" },
                values: new object[] { 2, "Tomasz", "Strzyżenie męskie" });

            migrationBuilder.InsertData(
                table: "Klienci",
                columns: new[] { "Id", "Imie", "Nazwisko", "Telefon" },
                values: new object[] { 1, "Anna", "Kowalska", "123456789" });

            migrationBuilder.InsertData(
                table: "Klienci",
                columns: new[] { "Id", "Imie", "Nazwisko", "Telefon" },
                values: new object[] { 2, "Jan", "Nowak", "987654321" });

            migrationBuilder.InsertData(
                table: "Uslugi",
                columns: new[] { "Id", "Cena", "Nazwa" },
                values: new object[] { 1, 50m, "Strzyżenie" });

            migrationBuilder.InsertData(
                table: "Uslugi",
                columns: new[] { "Id", "Cena", "Nazwa" },
                values: new object[] { 2, 150m, "Farbowanie" });

            migrationBuilder.InsertData(
                table: "Uslugi",
                columns: new[] { "Id", "Cena", "Nazwa" },
                values: new object[] { 3, 100m, "Stylizacja" });

            migrationBuilder.InsertData(
                table: "Harmonogramy",
                columns: new[] { "Id", "CzyDostepne", "Data", "DzienTygodnia", "FryzjerId", "GodzinaRozpoczecia", "GodzinaZakonczenia" },
                values: new object[] { 1, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poniedziałek", 1, "08:00", "16:00" });

            migrationBuilder.InsertData(
                table: "Harmonogramy",
                columns: new[] { "Id", "CzyDostepne", "Data", "DzienTygodnia", "FryzjerId", "GodzinaRozpoczecia", "GodzinaZakonczenia" },
                values: new object[] { 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wtorek", 2, "09:00", "17:00" });

            migrationBuilder.InsertData(
                table: "Rezerwacje",
                columns: new[] { "Id", "CzasRezerwacji", "DataRezerwacji", "FryzjerId", "KlientId", "UslugaId" },
                values: new object[] { 1, "10:00:00", new DateTime(2023, 10, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Rezerwacje",
                columns: new[] { "Id", "CzasRezerwacji", "DataRezerwacji", "FryzjerId", "KlientId", "UslugaId" },
                values: new object[] { 2, "12:00:00", new DateTime(2023, 10, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Harmonogramy_FryzjerId",
                table: "Harmonogramy",
                column: "FryzjerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_FryzjerId",
                table: "Rezerwacje",
                column: "FryzjerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_KlientId",
                table: "Rezerwacje",
                column: "KlientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_UslugaId",
                table: "Rezerwacje",
                column: "UslugaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Harmonogramy");

            migrationBuilder.DropTable(
                name: "Rezerwacje");

            migrationBuilder.DropTable(
                name: "Fryzjerzy");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Uslugi");
        }
    }
}
