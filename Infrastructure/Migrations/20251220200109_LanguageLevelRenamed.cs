using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LanguageLevelRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationRequirement_Vacancies_VacancyId",
                table: "EducationRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_JobExperienceRequirement_Vacancies_VacancyId",
                table: "JobExperienceRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageLevelRequirement_Vacancies_VacancyId",
                table: "LanguageLevelRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyRequirement_JobExperienceRequirement_JobExperienc~",
                table: "TechnologyRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageLevelRequirement",
                table: "LanguageLevelRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobExperienceRequirement",
                table: "JobExperienceRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationRequirement",
                table: "EducationRequirement");

            migrationBuilder.RenameTable(
                name: "LanguageLevelRequirement",
                newName: "LanguageLevelRequirements");

            migrationBuilder.RenameTable(
                name: "JobExperienceRequirement",
                newName: "JobExperienceRequirements");

            migrationBuilder.RenameTable(
                name: "EducationRequirement",
                newName: "EducationRequirements");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageLevelRequirement_VacancyId",
                table: "LanguageLevelRequirements",
                newName: "IX_LanguageLevelRequirements_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_JobExperienceRequirement_VacancyId",
                table: "JobExperienceRequirements",
                newName: "IX_JobExperienceRequirements_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationRequirement_VacancyId",
                table: "EducationRequirements",
                newName: "IX_EducationRequirements_VacancyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageLevelRequirements",
                table: "LanguageLevelRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobExperienceRequirements",
                table: "JobExperienceRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationRequirements",
                table: "EducationRequirements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationRequirements_Vacancies_VacancyId",
                table: "EducationRequirements",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobExperienceRequirements_Vacancies_VacancyId",
                table: "JobExperienceRequirements",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageLevelRequirements_Vacancies_VacancyId",
                table: "LanguageLevelRequirements",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyRequirement_JobExperienceRequirements_JobExperien~",
                table: "TechnologyRequirement",
                column: "JobExperienceRequirementId",
                principalTable: "JobExperienceRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationRequirements_Vacancies_VacancyId",
                table: "EducationRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_JobExperienceRequirements_Vacancies_VacancyId",
                table: "JobExperienceRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageLevelRequirements_Vacancies_VacancyId",
                table: "LanguageLevelRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyRequirement_JobExperienceRequirements_JobExperien~",
                table: "TechnologyRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageLevelRequirements",
                table: "LanguageLevelRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobExperienceRequirements",
                table: "JobExperienceRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationRequirements",
                table: "EducationRequirements");

            migrationBuilder.RenameTable(
                name: "LanguageLevelRequirements",
                newName: "LanguageLevelRequirement");

            migrationBuilder.RenameTable(
                name: "JobExperienceRequirements",
                newName: "JobExperienceRequirement");

            migrationBuilder.RenameTable(
                name: "EducationRequirements",
                newName: "EducationRequirement");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageLevelRequirements_VacancyId",
                table: "LanguageLevelRequirement",
                newName: "IX_LanguageLevelRequirement_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_JobExperienceRequirements_VacancyId",
                table: "JobExperienceRequirement",
                newName: "IX_JobExperienceRequirement_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationRequirements_VacancyId",
                table: "EducationRequirement",
                newName: "IX_EducationRequirement_VacancyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageLevelRequirement",
                table: "LanguageLevelRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobExperienceRequirement",
                table: "JobExperienceRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationRequirement",
                table: "EducationRequirement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationRequirement_Vacancies_VacancyId",
                table: "EducationRequirement",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobExperienceRequirement_Vacancies_VacancyId",
                table: "JobExperienceRequirement",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageLevelRequirement_Vacancies_VacancyId",
                table: "LanguageLevelRequirement",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyRequirement_JobExperienceRequirement_JobExperienc~",
                table: "TechnologyRequirement",
                column: "JobExperienceRequirementId",
                principalTable: "JobExperienceRequirement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
