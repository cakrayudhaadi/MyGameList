using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGameList.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterGameRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterGame",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterGame", x => new { x.CharactersId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_CharacterGame_character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "character",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterGame_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGame_GamesId",
                table: "CharacterGame",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterGame");
        }
    }
}
