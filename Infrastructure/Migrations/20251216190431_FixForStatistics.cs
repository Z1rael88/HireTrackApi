using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixForStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_ResumeId",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "Summary_TotalSummary",
                table: "Statistics",
                newName: "TotalSummary");

            migrationBuilder.RenameColumn(
                name: "Summary_LanguageLevelSummary",
                table: "Statistics",
                newName: "LanguageLevelSummary");

            migrationBuilder.RenameColumn(
                name: "Summary_ExperienceSummary",
                table: "Statistics",
                newName: "Statistics_ExperienceSummary");

            migrationBuilder.RenameColumn(
                name: "Summary_EducationSummary",
                table: "Statistics",
                newName: "Statistics_EducationSummary");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_ResumeId_VacancyId",
                table: "Statistics",
                columns: new[] { "ResumeId", "VacancyId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_ResumeId_VacancyId",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "TotalSummary",
                table: "Statistics",
                newName: "Summary_TotalSummary");

            migrationBuilder.RenameColumn(
                name: "Statistics_ExperienceSummary",
                table: "Statistics",
                newName: "Summary_ExperienceSummary");

            migrationBuilder.RenameColumn(
                name: "Statistics_EducationSummary",
                table: "Statistics",
                newName: "Summary_EducationSummary");

            migrationBuilder.RenameColumn(
                name: "LanguageLevelSummary",
                table: "Statistics",
                newName: "Summary_LanguageLevelSummary");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_ResumeId",
                table: "Statistics",
                column: "ResumeId");
        }
    }
}
