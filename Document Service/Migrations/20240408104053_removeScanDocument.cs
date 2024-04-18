using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentService.Migrations
{
    /// <inheritdoc />
    public partial class removeScanDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scans");

            migrationBuilder.AddColumn<byte[]>(
                name: "scan",
                table: "Passports",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "scan",
                table: "Educations",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "scan",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "scan",
                table: "Educations");

            migrationBuilder.CreateTable(
                name: "Scans",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    educationFile = table.Column<byte[]>(type: "bytea", nullable: true),
                    passportFile = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scans", x => x.id);
                    table.ForeignKey(
                        name: "FK_Scans_Abiturients_id",
                        column: x => x.id,
                        principalTable: "Abiturients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
