using Microsoft.EntityFrameworkCore.Migrations;

namespace HOHSI.Data.Migrations
{
    public partial class mappedImageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Exercises",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Exercises");
        }
    }
}
