using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codes1.Service.Data.Migrations
{
    public partial class NewKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Codes",
                table: "Codes");

            migrationBuilder.AddColumn<int>(
                name: "CodeId",
                table: "Codes",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "CodeModelCodeId",
                table: "CodeActivities",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Codes",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Codes",
                table: "Codes",
                column: "CodeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivities_Codes_CodeModelCodeId",
                table: "CodeActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Codes",
                table: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_CodeActivities_CodeModelCodeId",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "CodeModelCodeId",
                table: "CodeActivities");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Codes",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Codes",
                table: "Codes",
                column: "Code");
        }
    }
}
