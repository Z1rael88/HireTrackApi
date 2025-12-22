using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTechnology : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_JobExperiences_JobExperienceId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_JobExperienceId",
                table: "Technologies");

            migrationBuilder.AddColumn<int>(
                name: "JobExperienceRequirementId",
                table: "Technologies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_JobExperienceRequirementId",
                table: "Technologies",
                column: "JobExperienceRequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_JobExperiences_JobExperienceRequirementId",
                table: "Technologies",
                column: "JobExperienceRequirementId",
                principalTable: "JobExperiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_JobExperiences_JobExperienceRequirementId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_JobExperienceRequirementId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "JobExperienceRequirementId",
                table: "Technologies");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_JobExperienceId",
                table: "Technologies",
                column: "JobExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_JobExperiences_JobExperienceId",
                table: "Technologies",
                column: "JobExperienceId",
                principalTable: "JobExperiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
