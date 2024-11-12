using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class KorisnikSlikaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_VlasnikId",
                table: "Komentar");

            migrationBuilder.DropForeignKey(
                name: "FK_Objava_AspNetUsers_VlasnikId",
                table: "Objava");

            migrationBuilder.AddColumn<string>(
                name: "SlikaUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_AspNetUsers_VlasnikId",
                table: "Komentar",
                column: "VlasnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objava_AspNetUsers_VlasnikId",
                table: "Objava",
                column: "VlasnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_VlasnikId",
                table: "Komentar");

            migrationBuilder.DropForeignKey(
                name: "FK_Objava_AspNetUsers_VlasnikId",
                table: "Objava");

            migrationBuilder.DropColumn(
                name: "SlikaUrl",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    TempId1 = table.Column<string>(type: "text", nullable: false),
                    TempId2 = table.Column<string>(type: "text", nullable: false),
                    TempId3 = table.Column<string>(type: "text", nullable: false),
                    TempId4 = table.Column<string>(type: "text", nullable: false),
                    TempId5 = table.Column<string>(type: "text", nullable: false),
                    TempId6 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.UniqueConstraint("AK_Korisnik_TempId1", x => x.TempId1);
                    table.UniqueConstraint("AK_Korisnik_TempId2", x => x.TempId2);
                    table.UniqueConstraint("AK_Korisnik_TempId3", x => x.TempId3);
                    table.UniqueConstraint("AK_Korisnik_TempId4", x => x.TempId4);
                    table.UniqueConstraint("AK_Korisnik_TempId5", x => x.TempId5);
                    table.UniqueConstraint("AK_Korisnik_TempId6", x => x.TempId6);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Korisnik_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Korisnik",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Korisnik_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Korisnik",
                principalColumn: "TempId2",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Korisnik_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Korisnik",
                principalColumn: "TempId3",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Korisnik_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Korisnik",
                principalColumn: "TempId4",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_Korisnik_VlasnikId",
                table: "Komentar",
                column: "VlasnikId",
                principalTable: "Korisnik",
                principalColumn: "TempId5",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objava_Korisnik_VlasnikId",
                table: "Objava",
                column: "VlasnikId",
                principalTable: "Korisnik",
                principalColumn: "TempId6",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
