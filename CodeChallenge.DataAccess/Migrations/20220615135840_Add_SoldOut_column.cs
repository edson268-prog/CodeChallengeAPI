using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeChallenge.DataAccess.Migrations
{
    public partial class Add_SoldOut_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoldOut",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoldOut",
                table: "Product");
        }
    }
}
