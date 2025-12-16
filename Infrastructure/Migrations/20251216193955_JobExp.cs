using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobExp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobExperiences_Resumes_ResumeId",
                table: "JobExperiences");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "JobExperiences",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobExperiences_Resumes_ResumeId",
                table: "JobExperiences",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobExperiences_Resumes_ResumeId",
                table: "JobExperiences");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "JobExperiences",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_JobExperiences_Resumes_ResumeId",
                table: "JobExperiences",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");
        }
    }
}
