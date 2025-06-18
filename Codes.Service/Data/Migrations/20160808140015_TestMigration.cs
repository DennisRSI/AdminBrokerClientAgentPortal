using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codes1.Service.Data.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BulkCodeAudits",
                columns: table => new
                {
                    BulkCodeAuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    Errors = table.Column<string>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    Issuer = table.Column<string>(maxLength: 50, nullable: false),
                    OrigionalFileSent = table.Column<string>(nullable: true),
                    TotalFailed = table.Column<int>(nullable: false),
                    TotalProcessed = table.Column<int>(nullable: false),
                    TotalSent = table.Column<int>(nullable: false),
                    TotalSucceeded = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkCodeAudits", x => x.BulkCodeAuditId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulkCodeAudits");
        }
    }
}
