using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class codeactivityfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivities_Codes_CodeModelCodeId",
                table: "CodeActivities");

            migrationBuilder.DropIndex(
                name: "IX_CodeActivities_CodeModelCodeId",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "CodeModelCodeId",
                table: "CodeActivities");

            migrationBuilder.AddColumn<int>(
                name: "CodeId",
                table: "CodeActivities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CodeActivities_CodeId",
                table: "CodeActivities",
                column: "CodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivities_Codes_CodeId",
                table: "CodeActivities",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "CodeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivities_Codes_CodeId",
                table: "CodeActivities");

            migrationBuilder.DropIndex(
                name: "IX_CodeActivities_CodeId",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "CodeActivities");

            migrationBuilder.AddColumn<int>(
                name: "CodeModelCodeId",
                table: "CodeActivities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeActivities_CodeModelCodeId",
                table: "CodeActivities",
                column: "CodeModelCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivities_Codes_CodeModelCodeId",
                table: "CodeActivities",
                column: "CodeModelCodeId",
                principalTable: "Codes",
                principalColumn: "CodeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
