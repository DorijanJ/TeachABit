using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class VerifikacijaStatusMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verificiran",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "VerifikacijaStatusId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VerifikacijaStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifikacijaStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VerifikacijaStatusId",
                table: "AspNetUsers",
                column: "VerifikacijaStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_VerifikacijaStatus_VerifikacijaStatusId",
                table: "AspNetUsers",
                column: "VerifikacijaStatusId",
                principalTable: "VerifikacijaStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_VerifikacijaStatus_VerifikacijaStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "VerifikacijaStatus");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VerifikacijaStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerifikacijaStatusId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Verificiran",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
