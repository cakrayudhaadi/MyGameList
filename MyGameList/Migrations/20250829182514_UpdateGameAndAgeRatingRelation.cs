using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGameList.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameAndAgeRatingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_age_ratings_age_rating_id",
                table: "games");

            migrationBuilder.DropIndex(
                name: "IX_games_age_rating_id",
                table: "games");

            migrationBuilder.DropColumn(
                name: "age_rating_id",
                table: "games");

            migrationBuilder.CreateTable(
                name: "game_age_ratings",
                columns: table => new
                {
                    AgeRatingsId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_age_ratings", x => new { x.AgeRatingsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_game_age_ratings_age_ratings_AgeRatingsId",
                        column: x => x.AgeRatingsId,
                        principalTable: "age_ratings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_age_ratings_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_game_age_ratings_GamesId",
                table: "game_age_ratings",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_age_ratings");

            migrationBuilder.AddColumn<int>(
                name: "age_rating_id",
                table: "games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_games_age_rating_id",
                table: "games",
                column: "age_rating_id");

            migrationBuilder.AddForeignKey(
                name: "FK_games_age_ratings_age_rating_id",
                table: "games",
                column: "age_rating_id",
                principalTable: "age_ratings",
                principalColumn: "id");
        }
    }
}
