using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class CodeChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldCodeId",
                table: "UsedCodes",
                newName: "OldCodeActivityId");

            migrationBuilder.RenameColumn(
                name: "OldCodeId",
                table: "PendingCodes",
                newName: "OldCodeActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldCodeActivityId",
                table: "UsedCodes",
                newName: "OldCodeId");

            migrationBuilder.RenameColumn(
                name: "OldCodeActivityId",
                table: "PendingCodes",
                newName: "OldCodeId");
        }
    }
}
