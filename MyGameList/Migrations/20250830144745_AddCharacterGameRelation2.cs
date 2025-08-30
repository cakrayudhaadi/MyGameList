using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGameList.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterGameRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterGame_character_CharactersId",
                table: "CharacterGame");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterGame_games_GamesId",
                table: "CharacterGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterGame",
                table: "CharacterGame");

            migrationBuilder.RenameTable(
                name: "CharacterGame",
                newName: "game_characters");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterGame_GamesId",
                table: "game_characters",
                newName: "IX_game_characters_GamesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_game_characters",
                table: "game_characters",
                columns: new[] { "CharactersId", "GamesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_game_characters_character_CharactersId",
                table: "game_characters",
                column: "CharactersId",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_game_characters_games_GamesId",
                table: "game_characters",
                column: "GamesId",
                principalTable: "games",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_characters_character_CharactersId",
                table: "game_characters");

            migrationBuilder.DropForeignKey(
                name: "FK_game_characters_games_GamesId",
                table: "game_characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_game_characters",
                table: "game_characters");

            migrationBuilder.RenameTable(
                name: "game_characters",
                newName: "CharacterGame");

            migrationBuilder.RenameIndex(
                name: "IX_game_characters_GamesId",
                table: "CharacterGame",
                newName: "IX_CharacterGame_GamesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterGame",
                table: "CharacterGame",
                columns: new[] { "CharactersId", "GamesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterGame_character_CharactersId",
                table: "CharacterGame",
                column: "CharactersId",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterGame_games_GamesId",
                table: "CharacterGame",
                column: "GamesId",
                principalTable: "games",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
