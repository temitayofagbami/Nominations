using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IndividualNominationsAPI.Migrations
{
    public partial class InitialCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "awardcategory_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "location_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "nomination_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "suborg_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "AwardCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<string>(maxLength: 30, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<string>(maxLength: 30, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubOrg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<string>(maxLength: 30, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nomination",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Alias = table.Column<string>(maxLength: 30, nullable: false),
                    Headline = table.Column<string>(maxLength: 100, nullable: false),
                    DescriptionComments = table.Column<string>(maxLength: 255, nullable: false),
                    ImpactComments = table.Column<string>(maxLength: 255, nullable: false),
                    Winner = table.Column<bool>(nullable: false),
                    ReviewStatus = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<string>(maxLength: 30, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    AwardCategoryId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    SubOrgId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nomination_AwardCategory_AwardCategoryId",
                        column: x => x.AwardCategoryId,
                        principalTable: "AwardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nomination_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nomination_SubOrg_SubOrgId",
                        column: x => x.SubOrgId,
                        principalTable: "SubOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nomination_AwardCategoryId",
                table: "Nomination",
                column: "AwardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomination_LocationId",
                table: "Nomination",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomination_SubOrgId",
                table: "Nomination",
                column: "SubOrgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nomination");

            migrationBuilder.DropTable(
                name: "AwardCategory");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "SubOrg");

            migrationBuilder.DropSequence(
                name: "awardcategory_hilo");

            migrationBuilder.DropSequence(
                name: "location_hilo");

            migrationBuilder.DropSequence(
                name: "nomination_hilo");

            migrationBuilder.DropSequence(
                name: "suborg_hilo");
        }
    }
}
