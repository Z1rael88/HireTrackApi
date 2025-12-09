using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedKeyForVacancyResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancyResumes",
                table: "VacancyResumes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VacancyResumes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancyResumes",
                table: "VacancyResumes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResumes_VacancyId",
                table: "VacancyResumes",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancyResumes",
                table: "VacancyResumes");

            migrationBuilder.DropIndex(
                name: "IX_VacancyResumes_VacancyId",
                table: "VacancyResumes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VacancyResumes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancyResumes",
                table: "VacancyResumes",
                columns: new[] { "VacancyId", "ResumeId" });
        }
    }
}
