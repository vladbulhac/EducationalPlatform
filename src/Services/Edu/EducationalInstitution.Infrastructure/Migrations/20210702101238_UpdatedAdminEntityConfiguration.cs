using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EducationalInstitution.Infrastructure.Migrations
{
    public partial class UpdatedAdminEntityConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EducationalInstitutionID_AdminID",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Admins");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EducationalInstitutionID",
                table: "Admins",
                column: "EducationalInstitutionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Admins_EducationalInstitutionID",
                table: "Admins");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID",
                table: "Buildings",
                columns: new[] { "BuildingID", "EducationalInstitutionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EducationalInstitutionID_AdminID",
                table: "Admins",
                columns: new[] { "EducationalInstitutionID", "AdminID" },
                unique: true);
        }
    }
}