using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class RadionicaPoslanaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ObavijestPoslana",
                table: "Radionica",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObavijestPoslana",
                table: "Radionica");
        }
    }
}
