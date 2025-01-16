using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class TecajPlacanjeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KomentarReakcije_AspNetUsers_KorisnikId",
                table: "KomentarReakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_KomentarReakcije_Komentar_KomentarId",
                table: "KomentarReakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcije_AspNetUsers_KorisnikId",
                table: "ObjavaReakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcije_Objava_ObjavaId",
                table: "ObjavaReakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_TecajFavoriti_AspNetUsers_KorisnikId",
                table: "TecajFavoriti");

            migrationBuilder.DropForeignKey(
                name: "FK_TecajFavoriti_Tecaj_TecajId",
                table: "TecajFavoriti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TecajFavoriti",
                table: "TecajFavoriti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjavaReakcije",
                table: "ObjavaReakcije");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KomentarReakcije",
                table: "KomentarReakcije");

            migrationBuilder.RenameTable(
                name: "TecajFavoriti",
                newName: "TecajFavorit");

            migrationBuilder.RenameTable(
                name: "ObjavaReakcije",
                newName: "ObjavaReakcija");

            migrationBuilder.RenameTable(
                name: "KomentarReakcije",
                newName: "KomentarReakcija");

            migrationBuilder.RenameIndex(
                name: "IX_TecajFavoriti_TecajId",
                table: "TecajFavorit",
                newName: "IX_TecajFavorit_TecajId");

            migrationBuilder.RenameIndex(
                name: "IX_TecajFavoriti_KorisnikId",
                table: "TecajFavorit",
                newName: "IX_TecajFavorit_KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcije_ObjavaId",
                table: "ObjavaReakcija",
                newName: "IX_ObjavaReakcija_ObjavaId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcije_KorisnikId",
                table: "ObjavaReakcija",
                newName: "IX_ObjavaReakcija_KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_KomentarReakcije_KorisnikId",
                table: "KomentarReakcija",
                newName: "IX_KomentarReakcija_KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_KomentarReakcije_KomentarId",
                table: "KomentarReakcija",
                newName: "IX_KomentarReakcija_KomentarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TecajFavorit",
                table: "TecajFavorit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjavaReakcija",
                table: "ObjavaReakcija",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KomentarReakcija",
                table: "KomentarReakcija",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TecajPlacanje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KorisnikId = table.Column<string>(type: "text", nullable: false),
                    TecajId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecajPlacanje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TecajPlacanje_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TecajPlacanje_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TecajPlacanje_KorisnikId",
                table: "TecajPlacanje",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_TecajPlacanje_TecajId",
                table: "TecajPlacanje",
                column: "TecajId");

            migrationBuilder.AddForeignKey(
                name: "FK_KomentarReakcija_AspNetUsers_KorisnikId",
                table: "KomentarReakcija",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KomentarReakcija_Komentar_KomentarId",
                table: "KomentarReakcija",
                column: "KomentarId",
                principalTable: "Komentar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TecajFavorit_AspNetUsers_KorisnikId",
                table: "TecajFavorit",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TecajFavorit_Tecaj_TecajId",
                table: "TecajFavorit",
                column: "TecajId",
                principalTable: "Tecaj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KomentarReakcija_AspNetUsers_KorisnikId",
                table: "KomentarReakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_KomentarReakcija_Komentar_KomentarId",
                table: "KomentarReakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcija_AspNetUsers_KorisnikId",
                table: "ObjavaReakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjavaReakcija_Objava_ObjavaId",
                table: "ObjavaReakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_TecajFavorit_AspNetUsers_KorisnikId",
                table: "TecajFavorit");

            migrationBuilder.DropForeignKey(
                name: "FK_TecajFavorit_Tecaj_TecajId",
                table: "TecajFavorit");

            migrationBuilder.DropTable(
                name: "TecajPlacanje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TecajFavorit",
                table: "TecajFavorit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjavaReakcija",
                table: "ObjavaReakcija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KomentarReakcija",
                table: "KomentarReakcija");

            migrationBuilder.RenameTable(
                name: "TecajFavorit",
                newName: "TecajFavoriti");

            migrationBuilder.RenameTable(
                name: "ObjavaReakcija",
                newName: "ObjavaReakcije");

            migrationBuilder.RenameTable(
                name: "KomentarReakcija",
                newName: "KomentarReakcije");

            migrationBuilder.RenameIndex(
                name: "IX_TecajFavorit_TecajId",
                table: "TecajFavoriti",
                newName: "IX_TecajFavoriti_TecajId");

            migrationBuilder.RenameIndex(
                name: "IX_TecajFavorit_KorisnikId",
                table: "TecajFavoriti",
                newName: "IX_TecajFavoriti_KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcija_ObjavaId",
                table: "ObjavaReakcije",
                newName: "IX_ObjavaReakcije_ObjavaId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjavaReakcija_KorisnikId",
                table: "ObjavaReakcije",
                newName: "IX_ObjavaReakcije_KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_KomentarReakcija_KorisnikId",
                table: "KomentarReakcije",
                newName: "IX_KomentarReakcije_KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_KomentarReakcija_KomentarId",
                table: "KomentarReakcije",
                newName: "IX_KomentarReakcije_KomentarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TecajFavoriti",
                table: "TecajFavoriti",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjavaReakcije",
                table: "ObjavaReakcije",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KomentarReakcije",
                table: "KomentarReakcije",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KomentarReakcije_AspNetUsers_KorisnikId",
                table: "KomentarReakcije",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KomentarReakcije_Komentar_KomentarId",
                table: "KomentarReakcije",
                column: "KomentarId",
                principalTable: "Komentar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TecajFavoriti_AspNetUsers_KorisnikId",
                table: "TecajFavoriti",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TecajFavoriti_Tecaj_TecajId",
                table: "TecajFavoriti",
                column: "TecajId",
                principalTable: "Tecaj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
