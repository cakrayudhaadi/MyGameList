using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGameList.Migrations
{
    /// <inheritdoc />
    public partial class AddPeopleFixDevPubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rating",
                table: "publishers",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_publishers_rating",
                table: "publishers",
                newName: "IX_publishers_name");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "developers",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_developers_rating",
                table: "developers",
                newName: "IX_developers_name");

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender_id = table.Column<int>(type: "int", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.id);
                    table.ForeignKey(
                        name: "FK_people_genders_gender_id",
                        column: x => x.gender_id,
                        principalTable: "genders",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_people_gender_id",
                table: "people",
                column: "gender_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "publishers",
                newName: "rating");

            migrationBuilder.RenameIndex(
                name: "IX_publishers_name",
                table: "publishers",
                newName: "IX_publishers_rating");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "developers",
                newName: "rating");

            migrationBuilder.RenameIndex(
                name: "IX_developers_name",
                table: "developers",
                newName: "IX_developers_rating");
        }
    }
}
