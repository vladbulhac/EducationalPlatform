using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Notification.Infrastructure.Migrations
{
    public partial class ChangedEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipients",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "IssuedBy",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "RecipientID",
                table: "Recipients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Events",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "TriggeredByAction",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIssued",
                table: "Events",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "Events",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipients",
                table: "Recipients",
                columns: new[] { "Id", "EventID" });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_EventID",
                table: "Recipients",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Issuer_TimeIssued_TriggeredByAction",
                table: "Events",
                columns: new[] { "Issuer", "TimeIssued", "TriggeredByAction" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Name_Message_Url",
                table: "Events",
                columns: new[] { "Name", "Message", "Url" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipients",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_EventID",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Events_Issuer_TimeIssued_TriggeredByAction",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Name_Message_Url",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Recipients",
                newName: "RecipientID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "EventID");

            migrationBuilder.AlterColumn<string>(
                name: "TriggeredByAction",
                table: "Events",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIssued",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuedBy",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipients",
                table: "Recipients",
                columns: new[] { "EventID", "RecipientID" });
        }
    }
}