using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes.Service.Data.Migrations
{
    public partial class DeactivationFix33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeactivationReason",
                table: "Campaigns",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeactivationReason",
                table: "Campaigns",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
