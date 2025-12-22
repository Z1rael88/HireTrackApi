using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyRequirement_JobExperienceRequirements_JobExperien~",
                table: "TechnologyRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyRequirement_TechnologyTypes_TechnologyTypeId",
                table: "TechnologyRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnologyRequirement",
                table: "TechnologyRequirement");

            migrationBuilder.RenameTable(
                name: "TechnologyRequirement",
                newName: "TechnologyRequirements");

            migrationBuilder.RenameIndex(
                name: "IX_TechnologyRequirement_TechnologyTypeId",
                table: "TechnologyRequirements",
                newName: "IX_TechnologyRequirements_TechnologyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnologyRequirement_JobExperienceRequirementId",
                table: "TechnologyRequirements",
                newName: "IX_TechnologyRequirements_JobExperienceRequirementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnologyRequirements",
                table: "TechnologyRequirements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyRequirements_JobExperienceRequirements_JobExperie~",
                table: "TechnologyRequirements",
                column: "JobExperienceRequirementId",
                principalTable: "JobExperienceRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyRequirements_TechnologyTypes_TechnologyTypeId",
                table: "TechnologyRequirements",
                column: "TechnologyTypeId",
                principalTable: "TechnologyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyRequirements_JobExperienceRequirements_JobExperie~",
                table: "TechnologyRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyRequirements_TechnologyTypes_TechnologyTypeId",
                table: "TechnologyRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnologyRequirements",
                table: "TechnologyRequirements");

            migrationBuilder.RenameTable(
                name: "TechnologyRequirements",
                newName: "TechnologyRequirement");

            migrationBuilder.RenameIndex(
                name: "IX_TechnologyRequirements_TechnologyTypeId",
                table: "TechnologyRequirement",
                newName: "IX_TechnologyRequirement_TechnologyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnologyRequirements_JobExperienceRequirementId",
                table: "TechnologyRequirement",
                newName: "IX_TechnologyRequirement_JobExperienceRequirementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnologyRequirement",
                table: "TechnologyRequirement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyRequirement_JobExperienceRequirements_JobExperien~",
                table: "TechnologyRequirement",
                column: "JobExperienceRequirementId",
                principalTable: "JobExperienceRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyRequirement_TechnologyTypes_TechnologyTypeId",
                table: "TechnologyRequirement",
                column: "TechnologyTypeId",
                principalTable: "TechnologyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
