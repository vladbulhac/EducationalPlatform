using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitutionAPI.Migrations
{
    public partial class UniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_IsDisabled_EducationalInstitutionID_Description",
                table: "EducationalInstitutions");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "EducationalInstitutionID", "IsDisabled" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "EducationalInstitutionID", "IsDisabled" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Name",
                table: "EducationalInstitutions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_IsDisabled_EducationalInstitutionID_Description",
                table: "EducationalInstitutions",
                columns: new[] { "Name", "LocationID", "IsDisabled", "EducationalInstitutionID", "Description" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_IsDisabled_EducationalInstitutionID_Description",
                table: "EducationalInstitutions");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "EducationalInstitutionID", "IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "EducationalInstitutionID", "IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_IsDisabled_EducationalInstitutionID_Description",
                table: "EducationalInstitutions",
                columns: new[] { "Name", "LocationID", "IsDisabled", "EducationalInstitutionID", "Description" });
        }
    }
}