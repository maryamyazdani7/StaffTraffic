using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffTraffic.Migrations
{
    public partial class modifyTraffic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInDate",
                table: "Traffic");

            migrationBuilder.RenameColumn(
                name: "RegDate",
                table: "Traffic",
                newName: "OutDate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Traffic",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<DateTime>(
                name: "InDate",
                table: "Traffic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDate",
                table: "Traffic");

            migrationBuilder.RenameColumn(
                name: "OutDate",
                table: "Traffic",
                newName: "RegDate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Traffic",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInDate",
                table: "Traffic",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
