using Microsoft.EntityFrameworkCore.Migrations;

namespace Notification.Infrastructure.Migrations
{
    public partial class RenamedEventUrlToUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Events",
                newName: "Uri");

            migrationBuilder.RenameIndex(
                name: "IX_Events_Name_Message_Url",
                table: "Events",
                newName: "IX_Events_Name_Message_Uri");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uri",
                table: "Events",
                newName: "Url");

            migrationBuilder.RenameIndex(
                name: "IX_Events_Name_Message_Uri",
                table: "Events",
                newName: "IX_Events_Name_Message_Url");
        }
    }
}
