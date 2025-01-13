using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class PodKomentariMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NadKomentarId",
                table: "Komentar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_NadKomentarId",
                table: "Komentar",
                column: "NadKomentarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_Komentar_NadKomentarId",
                table: "Komentar",
                column: "NadKomentarId",
                principalTable: "Komentar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_Komentar_NadKomentarId",
                table: "Komentar");

            migrationBuilder.DropIndex(
                name: "IX_Komentar_NadKomentarId",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "NadKomentarId",
                table: "Komentar");
        }
    }
}
