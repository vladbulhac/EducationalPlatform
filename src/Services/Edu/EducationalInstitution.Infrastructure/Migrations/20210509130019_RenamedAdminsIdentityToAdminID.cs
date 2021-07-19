using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitution.Infrastructure.Migrations
{
    public partial class RenamedAdminsIdentityToAdminID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identity",
                table: "Admins",
                newName: "AdminID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdminID",
                table: "Admins",
                newName: "Identity");
        }
    }
}