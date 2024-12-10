using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospitalfinal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branslar",
                columns: table => new
                {
                    BransId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BransAd = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branslar", x => x.BransId);
                });

            migrationBuilder.CreateTable(
                name: "Hastalar",
                columns: table => new
                {
                    HastaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HastaAd = table.Column<string>(type: "TEXT", nullable: false),
                    HastaSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Tc = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Adres = table.Column<string>(type: "TEXT", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "DATE", nullable: true),
                    Sifre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastalar", x => x.HastaId);
                });

            migrationBuilder.CreateTable(
                name: "Sekreterler",
                columns: table => new
                {
                    SekreterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SekreterAd = table.Column<string>(type: "TEXT", nullable: false),
                    SekreterSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    SekreterTC = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Sifre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sekreterler", x => x.SekreterId);
                });

            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoktorAd = table.Column<string>(type: "TEXT", nullable: false),
                    DoktorSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    DoktorTC = table.Column<string>(type: "TEXT", nullable: false),
                    BransId = table.Column<int>(type: "INTEGER", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Sifre = table.Column<string>(type: "TEXT", nullable: false),
                    Uzmanlik = table.Column<string>(type: "TEXT", nullable: true),
                    Deneyim = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.DoktorId);
                    table.ForeignKey(
                        name: "FK_Doktorlar_Branslar_BransId",
                        column: x => x.BransId,
                        principalTable: "Branslar",
                        principalColumn: "BransId");
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HastaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "DATE", nullable: false),
                    RandevuSaati = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    OnayDurumu = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuId);
                    table.ForeignKey(
                        name: "FK_Randevular_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorId");
                    table.ForeignKey(
                        name: "FK_Randevular_Hastalar_HastaId",
                        column: x => x.HastaId,
                        principalTable: "Hastalar",
                        principalColumn: "HastaId");
                });

            migrationBuilder.CreateTable(
                name: "RandevuMusait",
                columns: table => new
                {
                    RandevuMusaitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false),
                    MusaitTarih = table.Column<DateTime>(type: "DATE", nullable: false),
                    MusaitSaat = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    Durum = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuMusait", x => x.RandevuMusaitId);
                    table.ForeignKey(
                        name: "FK_RandevuMusait_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_BransId",
                table: "Doktorlar",
                column: "BransId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_DoktorTC",
                table: "Doktorlar",
                column: "DoktorTC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_Email",
                table: "Doktorlar",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hastalar_Email",
                table: "Hastalar",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hastalar_Tc",
                table: "Hastalar",
                column: "Tc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hastalar_Telefon",
                table: "Hastalar",
                column: "Telefon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_DoktorId",
                table: "Randevular",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HastaId",
                table: "Randevular",
                column: "HastaId");

            migrationBuilder.CreateIndex(
                name: "IX_RandevuMusait_DoktorId",
                table: "RandevuMusait",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sekreterler_Email",
                table: "Sekreterler",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sekreterler_SekreterTC",
                table: "Sekreterler",
                column: "SekreterTC",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "RandevuMusait");

            migrationBuilder.DropTable(
                name: "Sekreterler");

            migrationBuilder.DropTable(
                name: "Hastalar");

            migrationBuilder.DropTable(
                name: "Doktorlar");

            migrationBuilder.DropTable(
                name: "Branslar");
        }
    }
}
