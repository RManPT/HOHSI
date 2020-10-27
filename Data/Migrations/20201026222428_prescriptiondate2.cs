using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HOHSI.Data.Migrations
{
    public partial class prescriptiondate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Prescriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAndTime",
                table: "Prescriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAndTime",
                table: "Prescriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Prescriptions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
