using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class CheckMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Tecaj",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Tecaj",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Cijena",
                table: "Radionica",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Radionica",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Tecaj");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Tecaj");

            migrationBuilder.DropColumn(
                name: "Cijena",
                table: "Radionica");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Radionica");
        }
    }
}
