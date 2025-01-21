using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachABit.Model.Migrations
{
    /// <inheritdoc />
    public partial class LekcijeMaknuoIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DO $$
        BEGIN
            IF NOT EXISTS (
                SELECT 1
                FROM information_schema.columns
                WHERE table_schema = 'backend' -- Specify the schema
                AND table_name = 'Lekcija' AND column_name = 'LastUpdatedDateTime'
            ) THEN
                ALTER TABLE backend.""Lekcija"" ADD COLUMN ""LastUpdatedDateTime"" timestamp with time zone;
            END IF;
        END;
        $$");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDateTime",
                table: "Lekcija");
        }
    }
}
