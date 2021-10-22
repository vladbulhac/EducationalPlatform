/*using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EducationalInstitution.Infrastructure.Migrations.EducationalInstitutionMigrations
{
    public partial class NewMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionEducationalInstitutionID",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name_IsDisabled_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID_IsDisabled",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EducationalInstitutionID_IsDisabled_AdminID",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "ParentInstitutionEducationalInstitutionID",
                table: "EducationalInstitutions",
                newName: "ParentInstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationalInstitutions_ParentInstitutionEducationalInstitutionID",
                table: "EducationalInstitutions",
                newName: "IX_EducationalInstitutions_ParentInstitutionId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "EducationalInstitutions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "Buildings",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "Admins",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_IsDisabled",
                table: "EducationalInstitutions",
                column: "IsDisabled");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "EducationalInstitutionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "Name", "LocationID", "EducationalInstitutionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID",
                table: "Buildings",
                columns: new[] { "BuildingID", "EducationalInstitutionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_IsDisabled",
                table: "Buildings",
                column: "IsDisabled");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EducationalInstitutionID_AdminID",
                table: "Admins",
                columns: new[] { "EducationalInstitutionID", "AdminID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_IsDisabled",
                table: "Admins",
                column: "IsDisabled");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionId",
                table: "EducationalInstitutions",
                column: "ParentInstitutionId",
                principalTable: "EducationalInstitutions",
                principalColumn: "EducationalInstitutionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionId",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_IsDisabled",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInstitutions_Name_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_BuildingID_EducationalInstitutionID",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_IsDisabled",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EducationalInstitutionID_AdminID",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_IsDisabled",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "ParentInstitutionId",
                table: "EducationalInstitutions",
                newName: "ParentInstitutionEducationalInstitutionID");

            migrationBuilder.RenameIndex(
                name: "IX_EducationalInstitutions_ParentInstitutionId",
                table: "EducationalInstitutions",
                newName: "IX_EducationalInstitutions_ParentInstitutionEducationalInstitutionID");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "EducationalInstitutions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "Buildings",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionEducationalInstitutionID",
                table: "EducationalInstitutions",
                column: "ParentInstitutionEducationalInstitutionID",
                principalTable: "EducationalInstitutions",
                principalColumn: "EducationalInstitutionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}*/