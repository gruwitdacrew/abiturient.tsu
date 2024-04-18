using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyService.Migrations
{
    /// <inheritdoc />
    public partial class addIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uuid",
                table: "Programs",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Programs",
                newName: "uuid");
        }
    }
}
