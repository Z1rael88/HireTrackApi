using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LanguageFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeLanguages_Languages_LanguageId",
                table: "ResumeLanguages");

            migrationBuilder.DropColumn(
                name: "LanguageLevel",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "LanguageId",
                table: "ResumeLanguages",
                newName: "LanguageLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ResumeLanguages_LanguageId",
                table: "ResumeLanguages",
                newName: "IX_ResumeLanguages_LanguageLevelId");

            migrationBuilder.RenameColumn(
                name: "LanguageName",
                table: "Languages",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "LanguageLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageLevels_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageLevels_LanguageId",
                table: "LanguageLevels",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeLanguages_LanguageLevels_LanguageLevelId",
                table: "ResumeLanguages",
                column: "LanguageLevelId",
                principalTable: "LanguageLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeLanguages_LanguageLevels_LanguageLevelId",
                table: "ResumeLanguages");

            migrationBuilder.DropTable(
                name: "LanguageLevels");

            migrationBuilder.RenameColumn(
                name: "LanguageLevelId",
                table: "ResumeLanguages",
                newName: "LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_ResumeLanguages_LanguageLevelId",
                table: "ResumeLanguages",
                newName: "IX_ResumeLanguages_LanguageId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Languages",
                newName: "LanguageName");

            migrationBuilder.AddColumn<int>(
                name: "LanguageLevel",
                table: "Languages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeLanguages_Languages_LanguageId",
                table: "ResumeLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
