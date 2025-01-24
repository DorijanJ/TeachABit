using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class KorisnikStatusMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikStatusId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KorisnikStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KorisnikStatusId",
                table: "AspNetUsers",
                column: "KorisnikStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_KorisnikStatus_KorisnikStatusId",
                table: "AspNetUsers",
                column: "KorisnikStatusId",
                principalTable: "KorisnikStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_KorisnikStatus_KorisnikStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "KorisnikStatus");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_KorisnikStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KorisnikStatusId",
                table: "AspNetUsers");
        }
    }
}
