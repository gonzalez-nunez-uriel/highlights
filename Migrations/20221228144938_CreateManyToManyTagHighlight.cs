using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Highlights.Migrations
{
    /// <inheritdoc />
    public partial class CreateManyToManyTagHighlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HighlightTag",
                columns: table => new
                {
                    HighlightsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighlightTag", x => new { x.HighlightsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_HighlightTag_Highlight_HighlightsId",
                        column: x => x.HighlightsId,
                        principalTable: "Highlight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HighlightTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HighlightTag_TagsId",
                table: "HighlightTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighlightTag");
        }
    }
}
