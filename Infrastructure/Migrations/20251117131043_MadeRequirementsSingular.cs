using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeRequirementsSingular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobExperienceRequirement_VacancyId",
                table: "JobExperienceRequirement");

            migrationBuilder.DropIndex(
                name: "IX_EducationRequirement_VacancyId",
                table: "EducationRequirement");

            migrationBuilder.CreateIndex(
                name: "IX_JobExperienceRequirement_VacancyId",
                table: "JobExperienceRequirement",
                column: "VacancyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationRequirement_VacancyId",
                table: "EducationRequirement",
                column: "VacancyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobExperienceRequirement_VacancyId",
                table: "JobExperienceRequirement");

            migrationBuilder.DropIndex(
                name: "IX_EducationRequirement_VacancyId",
                table: "EducationRequirement");

            migrationBuilder.CreateIndex(
                name: "IX_JobExperienceRequirement_VacancyId",
                table: "JobExperienceRequirement",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationRequirement_VacancyId",
                table: "EducationRequirement",
                column: "VacancyId");
        }
    }
}
