using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuDishApp.Migrations
{
    public partial class add_MenuDish_DishType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DishType",
                table: "MenuDish",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DishType",
                table: "MenuDish");
        }
    }
}
