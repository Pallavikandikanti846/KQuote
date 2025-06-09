using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace passion_project_http5226.Data.Migrations
{
    /// <inheritdoc />
    public partial class quote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    quote_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    drama_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    actor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    episode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotes", x => x.quote_id);
                    table.ForeignKey(
                        name: "FK_quotes_Dramas_drama_id",
                        column: x => x.drama_id,
                        principalTable: "Dramas",
                        principalColumn: "drama_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_quotes_drama_id",
                table: "quotes",
                column: "drama_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quotes");
        }
    }
}
