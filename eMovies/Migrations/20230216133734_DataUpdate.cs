using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMovies.Migrations
{
    /// <inheritdoc />
    public partial class DataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Actors_Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Actors_Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
