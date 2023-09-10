using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmPool.Migrations
{
    /// <inheritdoc />
    public partial class FilmsInCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Films_FilmId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_FilmId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Collections");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FilmsInCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsInCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmsInCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsInCollections_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmsInCollections_CollectionId",
                table: "FilmsInCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmsInCollections_FilmId",
                table: "FilmsInCollections",
                column: "FilmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmsInCollections");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_FilmId",
                table: "Collections",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Films_FilmId",
                table: "Collections",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");
        }
    }
}
