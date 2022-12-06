using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datalayer.Migrations
{
    public partial class multilingual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Rooms",
                newName: "DescriptionUz");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Rooms",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionRu",
                table: "Rooms",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DescriptionRu",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "DescriptionUz",
                table: "Rooms",
                newName: "Description");
        }
    }
}
