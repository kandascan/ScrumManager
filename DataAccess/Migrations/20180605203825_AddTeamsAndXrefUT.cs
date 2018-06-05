using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class AddTeamsAndXrefUT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    ProductOwnerId = table.Column<int>(nullable: true),
                    ProjectManagerId = table.Column<int>(nullable: true),
                    ScrumMasterId = table.Column<int>(nullable: true),
                    TeamName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "XrefUserTeam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    TeamId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XrefUserTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XrefUserTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XrefUserTeam_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamName",
                table: "Team",
                column: "TeamName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XrefUserTeam_TeamId",
                table: "XrefUserTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_XrefUserTeam_UserId",
                table: "XrefUserTeam",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XrefUserTeam");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
