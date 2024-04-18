using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyService.Migrations
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
                name: "Faculties",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDocumentTypes",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    levelId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDocumentTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_EducationDocumentTypes_Levels_levelId",
                        column: x => x.levelId,
                        principalTable: "Levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NextLevels",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "text", nullable: false),
                    levelId = table.Column<string>(type: "text", nullable: false),
                    nextLevelId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextLevels", x => x.uuid);
                    table.ForeignKey(
                        name: "FK_NextLevels_Levels_levelId",
                        column: x => x.levelId,
                        principalTable: "Levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NextLevels_Levels_nextLevelId",
                        column: x => x.nextLevelId,
                        principalTable: "Levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "text", nullable: false),
                    facultyId = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    educationForm = table.Column<string>(type: "text", nullable: false),
                    levelId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.uuid);
                    table.ForeignKey(
                        name: "FK_Programs_Faculties_facultyId",
                        column: x => x.facultyId,
                        principalTable: "Faculties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programs_Levels_levelId",
                        column: x => x.levelId,
                        principalTable: "Levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationDocumentTypes_levelId",
                table: "EducationDocumentTypes",
                column: "levelId");

            migrationBuilder.CreateIndex(
                name: "IX_NextLevels_levelId",
                table: "NextLevels",
                column: "levelId");

            migrationBuilder.CreateIndex(
                name: "IX_NextLevels_nextLevelId",
                table: "NextLevels",
                column: "nextLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_facultyId",
                table: "Programs",
                column: "facultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_levelId",
                table: "Programs",
                column: "levelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abiturients");

            migrationBuilder.DropTable(
                name: "EducationDocumentTypes");

            migrationBuilder.DropTable(
                name: "NextLevels");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
