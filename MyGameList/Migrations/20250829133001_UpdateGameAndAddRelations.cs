using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGameList.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameAndAddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age_rating_id",
                table: "games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "plot",
                table: "games",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "year_release",
                table: "games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "game_developers",
                columns: table => new
                {
                    DevelopersId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_developers", x => new { x.DevelopersId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_game_developers_developers_DevelopersId",
                        column: x => x.DevelopersId,
                        principalTable: "developers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_developers_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "game_genres",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_genres", x => new { x.GamesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_game_genres_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_genres_genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "game_modes",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    ModesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_modes", x => new { x.GamesId, x.ModesId });
                    table.ForeignKey(
                        name: "FK_game_modes_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_modes_modes_ModesId",
                        column: x => x.ModesId,
                        principalTable: "modes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "game_platforms",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PlatformsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_platforms", x => new { x.GamesId, x.PlatformsId });
                    table.ForeignKey(
                        name: "FK_game_platforms_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_platforms_platforms_PlatformsId",
                        column: x => x.PlatformsId,
                        principalTable: "platforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "game_producers",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    ProducersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_producers", x => new { x.GamesId, x.ProducersId });
                    table.ForeignKey(
                        name: "FK_game_producers_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_producers_people_ProducersId",
                        column: x => x.ProducersId,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "game_publishers",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PublishersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_publishers", x => new { x.GamesId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_game_publishers_games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_publishers_publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_games_age_rating_id",
                table: "games",
                column: "age_rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_developers_GamesId",
                table: "game_developers",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_game_genres_GenresId",
                table: "game_genres",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_game_modes_ModesId",
                table: "game_modes",
                column: "ModesId");

            migrationBuilder.CreateIndex(
                name: "IX_game_platforms_PlatformsId",
                table: "game_platforms",
                column: "PlatformsId");

            migrationBuilder.CreateIndex(
                name: "IX_game_producers_ProducersId",
                table: "game_producers",
                column: "ProducersId");

            migrationBuilder.CreateIndex(
                name: "IX_game_publishers_PublishersId",
                table: "game_publishers",
                column: "PublishersId");

            migrationBuilder.AddForeignKey(
                name: "FK_games_age_ratings_age_rating_id",
                table: "games",
                column: "age_rating_id",
                principalTable: "age_ratings",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_age_ratings_age_rating_id",
                table: "games");

            migrationBuilder.DropTable(
                name: "game_developers");

            migrationBuilder.DropTable(
                name: "game_genres");

            migrationBuilder.DropTable(
                name: "game_modes");

            migrationBuilder.DropTable(
                name: "game_platforms");

            migrationBuilder.DropTable(
                name: "game_producers");

            migrationBuilder.DropTable(
                name: "game_publishers");

            migrationBuilder.DropIndex(
                name: "IX_games_age_rating_id",
                table: "games");

            migrationBuilder.DropColumn(
                name: "age_rating_id",
                table: "games");

            migrationBuilder.DropColumn(
                name: "plot",
                table: "games");

            migrationBuilder.DropColumn(
                name: "year_release",
                table: "games");
        }
    }
}
