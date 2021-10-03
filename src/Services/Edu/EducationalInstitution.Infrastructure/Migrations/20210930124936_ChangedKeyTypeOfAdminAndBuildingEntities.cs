using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalInstitution.Infrastructure.Migrations
{
    public partial class ChangedKeyTypeOfAdminAndBuildingEntities : Migration
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
                    ParentInstitutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutions", x => x.EducationalInstitutionID);
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_EducationalInstitutions_ParentInstitutionId",
                        column: x => x.ParentInstitutionId,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EducationalInstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => new { x.Id, x.EducationalInstitutionID });
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationalInstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    DateForPermanentDeletion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => new { x.Id, x.EducationalInstitutionID });
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
                name: "IX_Admins_IsDisabled",
                table: "Admins",
                column: "IsDisabled");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_EducationalInstitutionID",
                table: "Buildings",
                column: "EducationalInstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_IsDisabled",
                table: "Buildings",
                column: "IsDisabled");

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
                name: "IX_EducationalInstitutions_ParentInstitutionId",
                table: "EducationalInstitutions",
                column: "ParentInstitutionId");
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
