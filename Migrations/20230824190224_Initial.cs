using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchEngine.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Url = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2023, 8, 24)),
                    TokenType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Url);
                    table.ForeignKey(
                        name: "FK_Sites_Tokens_TokenType",
                        column: x => x.TokenType,
                        principalTable: "Tokens",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Url = table.Column<string>(type: "text", nullable: false),
                    SiteUrl = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: false),
                    LastUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2023, 8, 24))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Url);
                    table.ForeignKey(
                        name: "FK_Pages_Sites_SiteUrl",
                        column: x => x.SiteUrl,
                        principalTable: "Sites",
                        principalColumn: "Url");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SiteUrl",
                table: "Pages",
                column: "SiteUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_TokenType",
                table: "Sites",
                column: "TokenType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
