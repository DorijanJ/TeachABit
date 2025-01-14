using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class LekcijeMigrationBrzinskiFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sadrzaj",
                table: "Tecaj",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VlasnikId",
                table: "Tecaj",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Lekcija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    Sadrzaj = table.Column<string>(type: "text", nullable: false),
                    TecajId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lekcija_Tecaj_TecajId",
                        column: x => x.TecajId,
                        principalTable: "Tecaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tecaj_VlasnikId",
                table: "Tecaj",
                column: "VlasnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Lekcija_TecajId",
                table: "Lekcija",
                column: "TecajId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tecaj_AspNetUsers_VlasnikId",
                table: "Tecaj",
                column: "VlasnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecaj_AspNetUsers_VlasnikId",
                table: "Tecaj");

            migrationBuilder.DropTable(
                name: "Lekcija");

            migrationBuilder.DropIndex(
                name: "IX_Tecaj_VlasnikId",
                table: "Tecaj");

            migrationBuilder.DropColumn(
                name: "Sadrzaj",
                table: "Tecaj");

            migrationBuilder.DropColumn(
                name: "VlasnikId",
                table: "Tecaj");
        }
    }
}
