using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitution.Infrastructure.Migrations
{
    public partial class NewIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "EntityAccess_IsDisabled",
                table: "EducationalInstitutions",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "EntityAccess_DateForPermanentDeletion",
                table: "EducationalInstitutions",
                newName: "DateForPermanentDeletion");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "EducationalInstitutions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Buildings",
                columns: new[] { "EntityAccess_IsDisabled", "EntityAccess_DateForPermanentDeletion" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Admins",
                columns: new[] { "EntityAccess_IsDisabled", "EntityAccess_DateForPermanentDeletion" });
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
                name: "IX_EducationalInstitutions_Name_LocationID_IsDisabled_EducationalInstitutionID_Description",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "EducationalInstitutions",
                newName: "EntityAccess_IsDisabled");

            migrationBuilder.RenameColumn(
                name: "DateForPermanentDeletion",
                table: "EducationalInstitutions",
                newName: "EntityAccess_DateForPermanentDeletion");

            migrationBuilder.AlterColumn<bool>(
                name: "EntityAccess_IsDisabled",
                table: "EducationalInstitutions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "EducationalInstitutionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "Buildings",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "Admins",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });
        }
    }
}