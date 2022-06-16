using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeChallenge.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AgeRestriction = table.Column<int>(type: "int", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "El juguete usa baterias", "Juguete de Baterias" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "El juguete esta fabricado de plastico", "Juguete de Plastico" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "El juguete es manejado a travez de un control remoto", "Juguete a Control Remoto" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "AgeRestriction", "Company", "Description", "Name", "Price", "ProductTypeId" },
                values: new object[] { 1, 0, "Hasbro", "Juguete de robot autobot que hace sonidos", "Optimus Prime", 100m, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
