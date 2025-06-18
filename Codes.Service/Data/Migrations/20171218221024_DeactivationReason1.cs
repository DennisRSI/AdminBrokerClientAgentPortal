using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class DeactivationReason1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeactivationReasion",
                table: "CodeRanges",
                newName: "DeactivationReason");

            migrationBuilder.RenameColumn(
                name: "DeactivationReasion",
                table: "Clients",
                newName: "DeactivationReason");

            migrationBuilder.RenameColumn(
                name: "DeactivationReasion",
                table: "Campaigns",
                newName: "DeactivationReason");

            migrationBuilder.RenameColumn(
                name: "DeactivationReasion",
                table: "Brokers",
                newName: "DeactivationReason");

            migrationBuilder.RenameColumn(
                name: "DeactivationReasion",
                table: "Agents",
                newName: "DeactivationReason");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeactivationReason",
                table: "CodeRanges",
                newName: "DeactivationReasion");

            migrationBuilder.RenameColumn(
                name: "DeactivationReason",
                table: "Clients",
                newName: "DeactivationReasion");

            migrationBuilder.RenameColumn(
                name: "DeactivationReason",
                table: "Campaigns",
                newName: "DeactivationReasion");

            migrationBuilder.RenameColumn(
                name: "DeactivationReason",
                table: "Brokers",
                newName: "DeactivationReasion");

            migrationBuilder.RenameColumn(
                name: "DeactivationReason",
                table: "Agents",
                newName: "DeactivationReasion");
        }
    }
}
