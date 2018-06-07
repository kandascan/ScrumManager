using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class PopulateUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Role",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleName",
                table: "Role",
                column: "RoleName",
                unique: true,
                filter: "[RoleName] IS NOT NULL");

            migrationBuilder.Sql(
                @"insert into [ScrumManager].[dbo].[Role] ([RoleName]) values ('Scrum master'), ('Project manager'), ('Product owner'), ('Developer'), ('Tester')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_RoleName",
                table: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Role",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
