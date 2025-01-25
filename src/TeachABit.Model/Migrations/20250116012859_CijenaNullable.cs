using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class CijenaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cijena",
                table: "Radionica",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "KomentarRadionica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sadrzaj = table.Column<string>(type: "text", nullable: false),
                    VlasnikId = table.Column<string>(type: "text", nullable: false),
                    RadionicaId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    NadKomentarId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentarRadionica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomentarRadionica_AspNetUsers_VlasnikId",
                        column: x => x.VlasnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentarRadionica_KomentarRadionica_NadKomentarId",
                        column: x => x.NadKomentarId,
                        principalTable: "KomentarRadionica",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KomentarRadionica_Radionica_RadionicaId",
                        column: x => x.RadionicaId,
                        principalTable: "Radionica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomentarRadionica_NadKomentarId",
                table: "KomentarRadionica",
                column: "NadKomentarId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarRadionica_RadionicaId",
                table: "KomentarRadionica",
                column: "RadionicaId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarRadionica_VlasnikId",
                table: "KomentarRadionica",
                column: "VlasnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarRadionica");

            migrationBuilder.AlterColumn<int>(
                name: "Cijena",
                table: "Radionica",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
