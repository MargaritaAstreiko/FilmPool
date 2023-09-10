using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmPool.Migrations
{
    /// <inheritdoc />
    public partial class NullFilmCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Films_FilmId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "FilmId",
                table: "Collections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Films_FilmId",
                table: "Collections",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Films_FilmId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "FilmId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Films_FilmId",
                table: "Collections",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
