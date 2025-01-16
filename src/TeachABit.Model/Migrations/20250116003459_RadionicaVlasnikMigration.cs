using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class RadionicaVlasnikMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VlasnikId",
                table: "Radionica",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Radionica_VlasnikId",
                table: "Radionica",
                column: "VlasnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Radionica_AspNetUsers_VlasnikId",
                table: "Radionica",
                column: "VlasnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Radionica_AspNetUsers_VlasnikId",
                table: "Radionica");

            migrationBuilder.DropIndex(
                name: "IX_Radionica_VlasnikId",
                table: "Radionica");

            migrationBuilder.DropColumn(
                name: "VlasnikId",
                table: "Radionica");
        }
    }
}
