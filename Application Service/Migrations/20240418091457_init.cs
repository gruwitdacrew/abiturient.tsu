using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationService.Migrations
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
                    accessToken = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    fullName = table.Column<string>(type: "text", nullable: false),
                    birthDate = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    nationality = table.Column<string>(type: "text", nullable: false),
                    managerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    series = table.Column<string>(type: "text", nullable: false),
                    numberpas = table.Column<string>(name: "number_pas", type: "text", nullable: false),
                    datepas = table.Column<string>(name: "date_pas", type: "text", nullable: false),
                    documenttype = table.Column<string>(name: "document_type", type: "text", nullable: false),
                    numberedu = table.Column<string>(name: "number_edu", type: "text", nullable: false),
                    dateedu = table.Column<string>(name: "date_edu", type: "text", nullable: false),
                    grade = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_Applications_Abiturients_id",
                        column: x => x.id,
                        principalTable: "Abiturients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPrograms",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    programId = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPrograms", x => x.id);
                    table.ForeignKey(
                        name: "FK_ApplicationPrograms_Applications_id",
                        column: x => x.id,
                        principalTable: "Applications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationPrograms_Programs_programId",
                        column: x => x.programId,
                        principalTable: "Programs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPrograms_programId",
                table: "ApplicationPrograms",
                column: "programId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationPrograms");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Abiturients");
        }
    }
}
