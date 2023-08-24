using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SearchEngine.Migrations
{
    /// <inheritdoc />
    public partial class CounterAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sites_Tokens_TokenType",
                table: "Sites");

            migrationBuilder.DropIndex(
                name: "IX_Sites_TokenType",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "Sites");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Sites",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 25),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 24));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Pages",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 25),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 24));

            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Entries = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Url = table.Column<string>(type: "text", nullable: false),
                    TokenType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counters_Pages_Url",
                        column: x => x.Url,
                        principalTable: "Pages",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Counters_Tokens_TokenType",
                        column: x => x.TokenType,
                        principalTable: "Tokens",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counters_TokenType",
                table: "Counters",
                column: "TokenType");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_Url",
                table: "Counters",
                column: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Sites",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 24),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 25));

            migrationBuilder.AddColumn<string>(
                name: "TokenType",
                table: "Sites",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdate",
                table: "Pages",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2023, 8, 24),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2023, 8, 25));

            migrationBuilder.CreateIndex(
                name: "IX_Sites_TokenType",
                table: "Sites",
                column: "TokenType");

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_Tokens_TokenType",
                table: "Sites",
                column: "TokenType",
                principalTable: "Tokens",
                principalColumn: "Type");
        }
    }
}
