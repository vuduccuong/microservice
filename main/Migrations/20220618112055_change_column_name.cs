using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace main.Migrations
{
    public partial class change_column_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_products_Name",
                table: "products",
                newName: "IX_products_Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "products",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_products_Title",
                table: "products",
                newName: "IX_products_Name");
        }
    }
}
