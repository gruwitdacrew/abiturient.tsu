using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentService.Migrations
{
    /// <inheritdoc />
    public partial class deleteAbiturient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Abiturients_id",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Abiturients_id",
                table: "Passports");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Abiturients_id",
                table: "Educations",
                column: "id",
                principalTable: "Abiturients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Abiturients_id",
                table: "Passports",
                column: "id",
                principalTable: "Abiturients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
