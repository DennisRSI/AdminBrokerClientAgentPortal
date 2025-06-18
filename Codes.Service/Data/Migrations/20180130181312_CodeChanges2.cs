using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class CodeChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrigionalCreationDate",
                table: "UsedCodes",
                newName: "CodeCreatedDate");

            migrationBuilder.RenameColumn(
                name: "OrigionalCreationDate",
                table: "PendingCodes",
                newName: "CodeCreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeCreatedDate",
                table: "UsedCodes",
                newName: "OrigionalCreationDate");

            migrationBuilder.RenameColumn(
                name: "CodeCreatedDate",
                table: "PendingCodes",
                newName: "OrigionalCreationDate");
        }
    }
}
