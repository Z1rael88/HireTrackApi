using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LanguageFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResumeLanguages");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Resumes");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "LanguageLevels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageLevels_ResumeId",
                table: "LanguageLevels",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageLevels_Resumes_ResumeId",
                table: "LanguageLevels",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageLevels_Resumes_ResumeId",
                table: "LanguageLevels");

            migrationBuilder.DropIndex(
                name: "IX_LanguageLevels_ResumeId",
                table: "LanguageLevels");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "LanguageLevels");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Resumes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Resumes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Resumes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ResumeLanguages",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "integer", nullable: false),
                    LanguageLevelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeLanguages", x => new { x.ResumeId, x.LanguageLevelId });
                    table.ForeignKey(
                        name: "FK_ResumeLanguages_LanguageLevels_LanguageLevelId",
                        column: x => x.LanguageLevelId,
                        principalTable: "LanguageLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeLanguages_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeLanguages_LanguageLevelId",
                table: "ResumeLanguages",
                column: "LanguageLevelId");
        }
    }
}
