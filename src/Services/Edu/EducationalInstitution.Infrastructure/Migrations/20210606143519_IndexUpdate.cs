using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitution.Infrastructure.Migrations
{
    public partial class IndexUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_IsDisabled_EducationalInstitutionID_Description",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EducationalInstitutionID",
                table: "Admins");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Name_IsDisabled_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "Name", "IsDisabled", "LocationID", "EducationalInstitutionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID_IsDisabled",
                table: "Buildings",
                columns: new[] { "BuildingID", "EducationalInstitutionID", "IsDisabled" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EducationalInstitutionID_IsDisabled_AdminID",
                table: "Admins",
                columns: new[] { "EducationalInstitutionID", "IsDisabled", "AdminID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name_IsDisabled_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID_IsDisabled",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EducationalInstitutionID_IsDisabled_AdminID",
                table: "Admins");

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

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EducationalInstitutionID",
                table: "Admins",
                column: "EducationalInstitutionID");
        }
    }
}