using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class AddLocalCrime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "london_crime_by_lsoa",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    borough = table.Column<string>(nullable: true),
                    lsoa_code = table.Column<string>(nullable: true),
                    major_category = table.Column<string>(nullable: true),
                    minor_category = table.Column<string>(nullable: true),
                    month = table.Column<int>(nullable: false),
                    value = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_london_crime_by_lsoa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "london_crime_by_lsoa");
        }
    }
}
