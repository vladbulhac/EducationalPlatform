using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationaInstitutionAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationalInstitutions",
                columns: table => new
                {
                    EduInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LocationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuildingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accessibility_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    Accessibility_ScheduledForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutions", x => x.EduInstitutionID);
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    IdentityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    EducationalInstitutionEduInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Accessibility_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    Accessibility_ScheduledForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.IdentityID);
                    table.ForeignKey(
                        name: "FK_Personnel_EducationalInstitutions_EducationalInstitutionEduInstitutionID",
                        column: x => x.EducationalInstitutionEduInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EduInstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    IdentityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    OfficeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accessibility_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    Accessibility_ScheduledForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EduInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.IdentityID);
                    table.ForeignKey(
                        name: "FK_Professors_EducationalInstitutions_EduInstitutionID",
                        column: x => x.EduInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EduInstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    IdentityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    Accessibility_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    Accessibility_ScheduledForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EduInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.IdentityID);
                    table.ForeignKey(
                        name: "FK_Students_EducationalInstitutions_EduInstitutionID",
                        column: x => x.EduInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EduInstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionsAttended",
                columns: table => new
                {
                    InstitutionAttendedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationalInstitutionEduInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfessorIdentityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentIdentityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionsAttended", x => x.InstitutionAttendedID);
                    table.ForeignKey(
                        name: "FK_InstitutionsAttended_EducationalInstitutions_EducationalInstitutionEduInstitutionID",
                        column: x => x.EducationalInstitutionEduInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EduInstitutionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstitutionsAttended_Professors_ProfessorIdentityID",
                        column: x => x.ProfessorIdentityID,
                        principalTable: "Professors",
                        principalColumn: "IdentityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstitutionsAttended_Students_StudentIdentityID",
                        column: x => x.StudentIdentityID,
                        principalTable: "Students",
                        principalColumn: "IdentityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Accessibility_ScheduledForPermanentDeletion_Accessibility_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "Accessibility_ScheduledForPermanentDeletion", "Accessibility_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_Name_Description_EduInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "Name", "Description", "EduInstitutionID" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_Name_Description_LocationID_BuildingID_EduInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "Name", "Description", "LocationID", "BuildingID", "EduInstitutionID" });

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionsAttended_EducationalInstitutionEduInstitutionID",
                table: "InstitutionsAttended",
                column: "EducationalInstitutionEduInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionsAttended_ProfessorIdentityID",
                table: "InstitutionsAttended",
                column: "ProfessorIdentityID");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionsAttended_StudentIdentityID",
                table: "InstitutionsAttended",
                column: "StudentIdentityID");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_Accessibility_ScheduledForPermanentDeletion_Accessibility_IsDisabled",
                table: "Personnel",
                columns: new[] { "Accessibility_ScheduledForPermanentDeletion", "Accessibility_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_EducationalInstitutionEduInstitutionID",
                table: "Personnel",
                column: "EducationalInstitutionEduInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Accessibility_ScheduledForPermanentDeletion_Accessibility_IsDisabled",
                table: "Professors",
                columns: new[] { "Accessibility_ScheduledForPermanentDeletion", "Accessibility_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_Professors_EduInstitutionID",
                table: "Professors",
                column: "EduInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_IdentityID_Rank_OfficeID",
                table: "Professors",
                columns: new[] { "IdentityID", "Rank", "OfficeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_OfficeID",
                table: "Professors",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Accessibility_ScheduledForPermanentDeletion_Accessibility_IsDisabled",
                table: "Students",
                columns: new[] { "Accessibility_ScheduledForPermanentDeletion", "Accessibility_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_EduInstitutionID",
                table: "Students",
                column: "EduInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdentityID_CurrentYear",
                table: "Students",
                columns: new[] { "IdentityID", "CurrentYear" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstitutionsAttended");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "EducationalInstitutions");
        }
    }
}
