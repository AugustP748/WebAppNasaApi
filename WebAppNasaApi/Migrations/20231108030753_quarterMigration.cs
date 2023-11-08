using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppNasaApi.Migrations
{
    /// <inheritdoc />
    public partial class quarterMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NasaImageDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    href = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    media_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nasa_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NasaImageDB", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NasaImageDB");
        }
    }
}
