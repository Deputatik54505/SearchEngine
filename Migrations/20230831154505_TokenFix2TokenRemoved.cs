using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchEngine.Migrations
{
    /// <inheritdoc />
    public partial class TokenFix2TokenRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counters_Tokens_TokenType",
                table: "Counters");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Counters_TokenType",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "Counters");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Counters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Counters");

            migrationBuilder.AddColumn<string>(
                name: "TokenType",
                table: "Counters",
                type: "text",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Counters_TokenType",
                table: "Counters",
                column: "TokenType");

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_Tokens_TokenType",
                table: "Counters",
                column: "TokenType",
                principalTable: "Tokens",
                principalColumn: "Type");
        }
    }
}
