using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "Sites",
                columns: table => new
                {
                    Url = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2023, 8, 27))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Url);
                });

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
                name: "Pages",
                columns: table => new
                {
                    Url = table.Column<string>(type: "text", nullable: false),
                    SiteUrl = table.Column<string>(type: "text", nullable: true),
                    Html = table.Column<string>(type: "text", nullable: false),
                    LastUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2023, 8, 27))
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

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SiteUrl",
                table: "Pages",
                column: "SiteUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
