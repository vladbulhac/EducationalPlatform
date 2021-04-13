using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitutionAPI.Migrations
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
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentInstitutionEduInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityAccess_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    EntityAccess_DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutions", x => x.EduInstitutionID);
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionEduInstitutionID",
                        column: x => x.ParentInstitutionEduInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EduInstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationalInstitutionsBuildings",
                columns: table => new
                {
                    EducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityAccess_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    EntityAccess_DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutionsBuildings", x => new { x.BuildingID, x.EducationalInstitutionID });
                    table.ForeignKey(
                        name: "FK_EducationalInstitutionsBuildings_EducationalInstitutions_EducationalInstitutionID",
                        column: x => x.EducationalInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EduInstitutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_EduInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "EduInstitutionID" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_ParentInstitutionEduInstitutionID",
                table: "EducationalInstitutions",
                column: "ParentInstitutionEduInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutionsBuildings_EducationalInstitutionID",
                table: "EducationalInstitutionsBuildings",
                column: "EducationalInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutionsBuildings_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "EducationalInstitutionsBuildings",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationalInstitutionsBuildings");

            migrationBuilder.DropTable(
                name: "EducationalInstitutions");
        }
    }
}
