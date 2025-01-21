using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class KomentarRadionicaReakcija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KomentarRadionicaReakcija",
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
                    table.PrimaryKey("PK_KomentarRadionicaReakcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomentarRadionicaReakcija_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentarRadionicaReakcija_KomentarRadionica_KomentarId",
                        column: x => x.KomentarId,
                        principalTable: "KomentarRadionica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomentarRadionicaReakcija_KomentarId",
                table: "KomentarRadionicaReakcija",
                column: "KomentarId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarRadionicaReakcija_KorisnikId",
                table: "KomentarRadionicaReakcija",
                column: "KorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarRadionicaReakcija");
        }
    }
}
