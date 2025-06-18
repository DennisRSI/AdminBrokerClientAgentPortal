using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codes1.Service.Data.Migrations
{
    public partial class CodeActivations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeActivations",
                columns: table => new
                {
                    CodeActivationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(maxLength: 255, nullable: true),
                    Address2 = table.Column<string>(maxLength: 255, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    CodeRangeId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 255, nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    RSIId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    Username = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeActivations", x => x.CodeActivationId);
                    table.ForeignKey(
                        name: "FK_CodeActivations_CodeRanges_CodeRangeId",
                        column: x => x.CodeRangeId,
                        principalTable: "CodeRanges",
                        principalColumn: "CodeRangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeActivations_CodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeActivations");
        }
    }
}
