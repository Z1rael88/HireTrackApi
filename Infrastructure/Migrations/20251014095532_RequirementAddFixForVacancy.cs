using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequirementAddFixForVacancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Vacancies");

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Vacancies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Technologies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EducationRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducationType = table.Column<int>(type: "integer", nullable: false),
                    Degree = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationRequirement_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobExperienceRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExperienceRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobExperienceRequirement_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageLevelRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevelRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageLevelRequirement_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnologyRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: false),
                    TechnologyTypeId = table.Column<int>(type: "integer", nullable: false),
                    JobExperienceRequirementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnologyRequirement_JobExperienceRequirement_JobExperienc~",
                        column: x => x.JobExperienceRequirementId,
                        principalTable: "JobExperienceRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnologyRequirement_TechnologyTypes_TechnologyTypeId",
                        column: x => x.TechnologyTypeId,
                        principalTable: "TechnologyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationRequirement_VacancyId",
                table: "EducationRequirement",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobExperienceRequirement_VacancyId",
                table: "JobExperienceRequirement",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageLevelRequirement_VacancyId",
                table: "LanguageLevelRequirement",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyRequirement_JobExperienceRequirementId",
                table: "TechnologyRequirement",
                column: "JobExperienceRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyRequirement_TechnologyTypeId",
                table: "TechnologyRequirement",
                column: "TechnologyTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationRequirement");

            migrationBuilder.DropTable(
                name: "LanguageLevelRequirement");

            migrationBuilder.DropTable(
                name: "TechnologyRequirement");

            migrationBuilder.DropTable(
                name: "JobExperienceRequirement");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Technologies");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Vacancies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
