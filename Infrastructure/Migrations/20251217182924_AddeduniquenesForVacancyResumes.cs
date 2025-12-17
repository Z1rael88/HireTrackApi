using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddeduniquenesForVacancyResumes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VacancyResumes_ResumeId",
                table: "VacancyResumes");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResumes_ResumeId_VacancyId",
                table: "VacancyResumes",
                columns: new[] { "ResumeId", "VacancyId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VacancyResumes_ResumeId_VacancyId",
                table: "VacancyResumes");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResumes_ResumeId",
                table: "VacancyResumes",
                column: "ResumeId");
        }
    }
}
