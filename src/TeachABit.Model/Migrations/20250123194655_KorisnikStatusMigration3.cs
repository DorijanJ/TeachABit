﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class KorisnikStatusMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");
        }
    }
}
