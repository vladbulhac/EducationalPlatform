using Microsoft.EntityFrameworkCore.Migrations;

namespace Notification.Infrastructure.Migrations
{
    public partial class ChangedEventIdToStringType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TriggeredByAction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Issuer = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TimeIssued = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => new { x.Id, x.EventId });
                    table.ForeignKey(
                        name: "FK_Recipients_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Issuer_TimeIssued_TriggeredByAction",
                table: "Events",
                columns: new[] { "Issuer", "TimeIssued", "TriggeredByAction" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Name_Message_Url",
                table: "Events",
                columns: new[] { "Name", "Message", "Url" });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_EventId",
                table: "Recipients",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}