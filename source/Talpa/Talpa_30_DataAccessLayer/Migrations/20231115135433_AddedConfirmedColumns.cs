using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedConfirmedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfirmedOutingDateId",
                table: "Outings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmedSuggestionId",
                table: "Outings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedOutingDateId",
                table: "Outings");

            migrationBuilder.DropColumn(
                name: "ConfirmedSuggestionId",
                table: "Outings");
        }
    }
}
