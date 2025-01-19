using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDateTimeObjava : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sadrzaj",
                table: "Tecaj");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Objava",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Objava");

            migrationBuilder.AddColumn<string>(
                name: "Sadrzaj",
                table: "Tecaj",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
