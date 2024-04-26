using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyService.Migrations
{
    /// <inheritdoc />
    public partial class deleteAbiturient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abiturients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abiturients",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    accessToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturients", x => x.id);
                });
        }
    }
}
