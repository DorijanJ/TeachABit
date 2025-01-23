using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class AddRadionicaOcjena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RadionicaOcjena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RadionicaId = table.Column<int>(type: "integer", nullable: false),
                    KorisnikId = table.Column<string>(type: "text", nullable: false),
                    Ocjena = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadionicaOcjena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadionicaOcjena_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RadionicaOcjena_Radionica_RadionicaId",
                        column: x => x.RadionicaId,
                        principalTable: "Radionica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RadionicaOcjena_KorisnikId",
                table: "RadionicaOcjena",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_RadionicaOcjena_RadionicaId",
                table: "RadionicaOcjena",
                column: "RadionicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RadionicaOcjena");
        }
    }
}
