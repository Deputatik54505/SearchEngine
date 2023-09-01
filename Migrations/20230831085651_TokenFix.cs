using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchEngine.Migrations
{
    /// <inheritdoc />
    public partial class TokenFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Sites",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 31),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 27));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Pages",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 31),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 27));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Sites",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 27),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 31));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Pages",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 27),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 31));
        }
    }
}
