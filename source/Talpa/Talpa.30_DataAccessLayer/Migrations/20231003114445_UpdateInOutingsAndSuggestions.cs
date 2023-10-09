using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInOutingsAndSuggestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suggestions_Outings_OutingId",
                table: "Suggestions");

            migrationBuilder.DropIndex(
                name: "IX_Suggestions_OutingId",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "OutingId",
                table: "Suggestions");

            migrationBuilder.CreateTable(
                name: "OutingSuggestion",
                columns: table => new
                {
                    OutingsId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutingSuggestion", x => new { x.OutingsId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_OutingSuggestion_Outings_OutingsId",
                        column: x => x.OutingsId,
                        principalTable: "Outings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutingSuggestion_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OutingSuggestion_SuggestionsId",
                table: "OutingSuggestion",
                column: "SuggestionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutingSuggestion");

            migrationBuilder.AddColumn<int>(
                name: "OutingId",
                table: "Suggestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_OutingId",
                table: "Suggestions",
                column: "OutingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suggestions_Outings_OutingId",
                table: "Suggestions",
                column: "OutingId",
                principalTable: "Outings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
