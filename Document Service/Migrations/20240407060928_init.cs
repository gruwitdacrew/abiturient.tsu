using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    documentType = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<string>(type: "text", nullable: false),
                    grade = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Educations_Abiturients_id",
                        column: x => x.id,
                        principalTable: "Abiturients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    series = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Passports_Abiturients_id",
                        column: x => x.id,
                        principalTable: "Abiturients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropTable(
                name: "Scans");

            migrationBuilder.DropTable(
                name: "Abiturients");
        }
    }
}
