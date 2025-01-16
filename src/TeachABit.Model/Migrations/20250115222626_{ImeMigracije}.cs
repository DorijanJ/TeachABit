using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class ImeMigracije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KomentarTecaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sadrzaj = table.Column<string>(type: "text", nullable: false),
                    VlasnikId = table.Column<string>(type: "text", nullable: false),
                    TecajId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    NadKomentarId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentarTecaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomentarTecaj_AspNetUsers_VlasnikId",
                        column: x => x.VlasnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentarTecaj_KomentarTecaj_NadKomentarId",
                        column: x => x.NadKomentarId,
                        principalTable: "KomentarTecaj",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KomentarTecaj_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KomentarTecajReakcija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KorisnikId = table.Column<string>(type: "text", nullable: false),
                    KomentarId = table.Column<int>(type: "integer", nullable: false),
                    Liked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentarTecajReakcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomentarTecajReakcija_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentarTecajReakcija_KomentarTecaj_KomentarId",
                        column: x => x.KomentarId,
                        principalTable: "KomentarTecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomentarTecaj_NadKomentarId",
                table: "KomentarTecaj",
                column: "NadKomentarId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarTecaj_TecajId",
                table: "KomentarTecaj",
                column: "TecajId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarTecaj_VlasnikId",
                table: "KomentarTecaj",
                column: "VlasnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarTecajReakcija_KomentarId",
                table: "KomentarTecajReakcija",
                column: "KomentarId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarTecajReakcija_KorisnikId",
                table: "KomentarTecajReakcija",
                column: "KorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarTecajReakcija");

            migrationBuilder.DropTable(
                name: "KomentarTecaj");
        }
    }
}
