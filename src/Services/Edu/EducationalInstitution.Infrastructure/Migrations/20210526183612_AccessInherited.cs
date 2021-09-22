/*using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitution.Infrastructure.Migrations
{
    public partial class AccessInherited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buildings_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Admins_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "EntityAccess_IsDisabled",
                table: "Buildings",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "EntityAccess_DateForPermanentDeletion",
                table: "Buildings",
                newName: "DateForPermanentDeletion");

            migrationBuilder.RenameColumn(
                name: "EntityAccess_IsDisabled",
                table: "Admins",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "EntityAccess_DateForPermanentDeletion",
                table: "Admins",
                newName: "DateForPermanentDeletion");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Buildings",
                newName: "EntityAccess_IsDisabled");

            migrationBuilder.RenameColumn(
                name: "DateForPermanentDeletion",
                table: "Buildings",
                newName: "EntityAccess_DateForPermanentDeletion");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Admins",
                newName: "EntityAccess_IsDisabled");

            migrationBuilder.RenameColumn(
                name: "DateForPermanentDeletion",
                table: "Admins",
                newName: "EntityAccess_DateForPermanentDeletion");

            migrationBuilder.AlterColumn<bool>(
                name: "EntityAccess_IsDisabled",
                table: "Buildings",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "EntityAccess_IsDisabled",
                table: "Admins",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Buildings",
                columns: new[] { "EntityAccess_IsDisabled", "EntityAccess_DateForPermanentDeletion" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EntityAccess_IsDisabled_EntityAccess_DateForPermanentDeletion",
                table: "Admins",
                columns: new[] { "EntityAccess_IsDisabled", "EntityAccess_DateForPermanentDeletion" });
        }
    }
}*/