using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class ObjavaReakcijaPopravakMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcija_AspNetUsers_KorisnikId",
                table: "ObjavaReakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcija_Objava_ObjavaId",
                table: "ObjavaReakcija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjavaReakcija",
                table: "ObjavaReakcija");

            migrationBuilder.RenameTable(
                name: "ObjavaReakcija",
                newName: "ObjavaReakcije");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcija_ObjavaId",
                table: "ObjavaReakcije",
                newName: "IX_ObjavaReakcije_ObjavaId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcija_KorisnikId",
                table: "ObjavaReakcije",
                newName: "IX_ObjavaReakcije_KorisnikId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjavaReakcije",
                table: "ObjavaReakcije",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjavaReakcije_AspNetUsers_KorisnikId",
                table: "ObjavaReakcije",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjavaReakcije_Objava_ObjavaId",
                table: "ObjavaReakcije",
                column: "ObjavaId",
                principalTable: "Objava",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcije_AspNetUsers_KorisnikId",
                table: "ObjavaReakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcije_Objava_ObjavaId",
                table: "ObjavaReakcije");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjavaReakcije",
                table: "ObjavaReakcije");

            migrationBuilder.RenameTable(
                name: "ObjavaReakcije",
                newName: "ObjavaReakcija");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcije_ObjavaId",
                table: "ObjavaReakcija",
                newName: "IX_ObjavaReakcija_ObjavaId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcije_KorisnikId",
                table: "ObjavaReakcija",
                newName: "IX_ObjavaReakcija_KorisnikId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjavaReakcija",
                table: "ObjavaReakcija",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjavaReakcija_AspNetUsers_KorisnikId",
                table: "ObjavaReakcija",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjavaReakcija_Objava_ObjavaId",
                table: "ObjavaReakcija",
                column: "ObjavaId",
                principalTable: "Objava",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
