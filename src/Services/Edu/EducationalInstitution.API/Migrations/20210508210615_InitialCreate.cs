using Microsoft.EntityFrameworkCore.Migrations;
using System;

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
                    EducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentInstitutionEducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityAccess_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    EntityAccess_DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutions", x => x.EducationalInstitutionID);
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionEducationalInstitutionID",
                        column: x => x.ParentInstitutionEducationalInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EducationalInstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Identity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityAccess_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    EntityAccess_DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => new { x.Identity, x.EducationalInstitutionID });
                    table.ForeignKey(
                        name: "FK_Admins_EducationalInstitutions_EducationalInstitutionID",
                        column: x => x.EducationalInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EducationalInstitutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    EducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityAccess_IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    EntityAccess_DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => new { x.BuildingID, x.EducationalInstitutionID });
                    table.ForeignKey(
                        name: "FK_Buildings_EducationalInstitutions_EducationalInstitutionID",
                        column: x => x.EducationalInstitutionID,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EducationalInstitutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EducationalInstitutionID",
                table: "Admins",
                column: "EducationalInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "Admins",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_EducationalInstitutionID",
                table: "Buildings",
                column: "EducationalInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "Buildings",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_EntityAccess_DateForPermanentDeletion_EntityAccess_IsDisabled",
                table: "EducationalInstitutions",
                columns: new[] { "EntityAccess_DateForPermanentDeletion", "EntityAccess_IsDisabled" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationID_EducationalInstitutionID",
                table: "EducationalInstitutions",
                columns: new[] { "LocationID", "EducationalInstitutionID" });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_ParentInstitutionEducationalInstitutionID",
                table: "EducationalInstitutions",
                column: "ParentInstitutionEducationalInstitutionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "EducationalInstitutions");
        }
    }
}