using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class PopravciZaTecajeveMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ocjena",
                table: "KorisnikTecajOcjene",
                newName: "Ocjena");

            migrationBuilder.AlterColumn<int>(
                name: "Ocjena",
                table: "KorisnikTecajOcjene",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ocjena",
                table: "KorisnikTecajOcjene",
                newName: "ocjena");

            migrationBuilder.AlterColumn<double>(
                name: "ocjena",
                table: "KorisnikTecajOcjene",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
