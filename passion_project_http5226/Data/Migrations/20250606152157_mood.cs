using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace passion_project_http5226.Data.Migrations
{
    /// <inheritdoc />
    public partial class mood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "moods",
                columns: table => new
                {
                    mood_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moods", x => x.mood_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "moods");
        }
    }
}
