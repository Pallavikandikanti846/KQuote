using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace passion_project_http5226.Data.Migrations
{
    /// <inheritdoc />
    public partial class drama : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dramas",
                columns: table => new
                {
                    drama_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    release_year = table.Column<int>(type: "int", nullable: false),
                    genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    synopsis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dramas", x => x.drama_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dramas");
        }
    }
}
