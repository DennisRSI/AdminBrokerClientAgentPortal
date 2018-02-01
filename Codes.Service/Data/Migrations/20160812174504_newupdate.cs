using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class newupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Codes");

            migrationBuilder.AddColumn<string>(
                name: "CreatorIP",
                table: "Codes",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HotelPoints",
                table: "Codes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Codes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyEmail",
                table: "Codes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "CodeActivities",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "CodeActivities",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "CodeActivities",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CodeActivities",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "CodeActivities",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CodeActivities",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "CodeActivities",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CodeActivities",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "CodeActivities",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "CodeActivities",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "CodeActivities",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "CodeActivities",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "CodeActivities",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateCode",
                table: "CodeActivities",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "CodeActivities",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "BulkCodeAudits",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BulkCodeAudits",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BulkCodeAudits",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Codes",
                maxLength: 500,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "CodeActivities",
                maxLength: 100,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorIP",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "HotelPoints",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "VerifyEmail",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "StateCode",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "BulkCodeAudits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BulkCodeAudits");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BulkCodeAudits");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Codes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Codes",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "CodeActivities",
                nullable: false);
        }
    }
}
