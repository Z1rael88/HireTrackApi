using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalMatchPercent = table.Column<double>(type: "double precision", nullable: false),
                    Summary_TotalSummary = table.Column<string>(type: "text", nullable: false),
                    Summary_EducationSummary = table.Column<string>(type: "text", nullable: false),
                    Summary_ExperienceSummary = table.Column<string>(type: "text", nullable: false),
                    Summary_LanguageLevelSummary = table.Column<string>(type: "text", nullable: false),
                    LanguageMatchPercent = table.Column<double>(type: "double precision", nullable: false),
                    LanguageSummary = table.Column<string>(type: "text", nullable: false),
                    EducationMatchPercent = table.Column<double>(type: "double precision", nullable: false),
                    EducationSummary = table.Column<string>(type: "text", nullable: false),
                    ExperienceMatchPercent = table.Column<double>(type: "double precision", nullable: false),
                    ExperienceSummary = table.Column<string>(type: "text", nullable: false),
                    ResumeId = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_ResumeId",
                table: "Statistics",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_VacancyId",
                table: "Statistics",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
