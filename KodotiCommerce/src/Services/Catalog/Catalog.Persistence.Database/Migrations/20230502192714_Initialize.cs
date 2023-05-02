using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Persistence.Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 1", "Product 1", 888m },
                    { 73, "Description for product 73", "Product 73", 482m },
                    { 72, "Description for product 72", "Product 72", 178m },
                    { 71, "Description for product 71", "Product 71", 783m },
                    { 70, "Description for product 70", "Product 70", 408m },
                    { 69, "Description for product 69", "Product 69", 459m },
                    { 68, "Description for product 68", "Product 68", 796m },
                    { 67, "Description for product 67", "Product 67", 991m },
                    { 66, "Description for product 66", "Product 66", 262m },
                    { 65, "Description for product 65", "Product 65", 753m },
                    { 64, "Description for product 64", "Product 64", 968m },
                    { 63, "Description for product 63", "Product 63", 424m },
                    { 62, "Description for product 62", "Product 62", 368m },
                    { 61, "Description for product 61", "Product 61", 907m },
                    { 60, "Description for product 60", "Product 60", 394m },
                    { 59, "Description for product 59", "Product 59", 717m },
                    { 58, "Description for product 58", "Product 58", 873m },
                    { 57, "Description for product 57", "Product 57", 200m },
                    { 56, "Description for product 56", "Product 56", 378m },
                    { 55, "Description for product 55", "Product 55", 223m },
                    { 54, "Description for product 54", "Product 54", 971m },
                    { 53, "Description for product 53", "Product 53", 610m },
                    { 74, "Description for product 74", "Product 74", 303m },
                    { 52, "Description for product 52", "Product 52", 657m },
                    { 75, "Description for product 75", "Product 75", 940m },
                    { 77, "Description for product 77", "Product 77", 388m },
                    { 98, "Description for product 98", "Product 98", 353m },
                    { 97, "Description for product 97", "Product 97", 518m },
                    { 96, "Description for product 96", "Product 96", 401m },
                    { 95, "Description for product 95", "Product 95", 229m },
                    { 94, "Description for product 94", "Product 94", 913m },
                    { 93, "Description for product 93", "Product 93", 210m },
                    { 92, "Description for product 92", "Product 92", 154m },
                    { 91, "Description for product 91", "Product 91", 340m },
                    { 90, "Description for product 90", "Product 90", 819m },
                    { 89, "Description for product 89", "Product 89", 159m },
                    { 88, "Description for product 88", "Product 88", 177m },
                    { 87, "Description for product 87", "Product 87", 941m },
                    { 86, "Description for product 86", "Product 86", 911m },
                    { 85, "Description for product 85", "Product 85", 699m },
                    { 84, "Description for product 84", "Product 84", 517m },
                    { 83, "Description for product 83", "Product 83", 296m },
                    { 82, "Description for product 82", "Product 82", 119m },
                    { 81, "Description for product 81", "Product 81", 137m },
                    { 80, "Description for product 80", "Product 80", 959m },
                    { 79, "Description for product 79", "Product 79", 966m },
                    { 78, "Description for product 78", "Product 78", 960m },
                    { 76, "Description for product 76", "Product 76", 936m },
                    { 51, "Description for product 51", "Product 51", 325m },
                    { 50, "Description for product 50", "Product 50", 395m },
                    { 49, "Description for product 49", "Product 49", 319m },
                    { 22, "Description for product 22", "Product 22", 610m },
                    { 21, "Description for product 21", "Product 21", 778m },
                    { 20, "Description for product 20", "Product 20", 610m },
                    { 19, "Description for product 19", "Product 19", 340m },
                    { 18, "Description for product 18", "Product 18", 691m },
                    { 17, "Description for product 17", "Product 17", 906m },
                    { 16, "Description for product 16", "Product 16", 711m },
                    { 15, "Description for product 15", "Product 15", 749m },
                    { 14, "Description for product 14", "Product 14", 556m },
                    { 13, "Description for product 13", "Product 13", 979m },
                    { 12, "Description for product 12", "Product 12", 140m },
                    { 11, "Description for product 11", "Product 11", 714m },
                    { 10, "Description for product 10", "Product 10", 321m },
                    { 9, "Description for product 9", "Product 9", 893m },
                    { 8, "Description for product 8", "Product 8", 238m },
                    { 7, "Description for product 7", "Product 7", 784m },
                    { 6, "Description for product 6", "Product 6", 785m },
                    { 5, "Description for product 5", "Product 5", 394m },
                    { 4, "Description for product 4", "Product 4", 144m },
                    { 3, "Description for product 3", "Product 3", 338m },
                    { 2, "Description for product 2", "Product 2", 912m },
                    { 23, "Description for product 23", "Product 23", 811m },
                    { 24, "Description for product 24", "Product 24", 465m },
                    { 25, "Description for product 25", "Product 25", 427m },
                    { 26, "Description for product 26", "Product 26", 999m },
                    { 48, "Description for product 48", "Product 48", 858m },
                    { 47, "Description for product 47", "Product 47", 962m },
                    { 46, "Description for product 46", "Product 46", 467m },
                    { 45, "Description for product 45", "Product 45", 498m },
                    { 44, "Description for product 44", "Product 44", 851m },
                    { 43, "Description for product 43", "Product 43", 322m },
                    { 42, "Description for product 42", "Product 42", 842m },
                    { 41, "Description for product 41", "Product 41", 669m },
                    { 40, "Description for product 40", "Product 40", 136m },
                    { 39, "Description for product 39", "Product 39", 308m },
                    { 99, "Description for product 99", "Product 99", 197m },
                    { 38, "Description for product 38", "Product 38", 827m },
                    { 36, "Description for product 36", "Product 36", 440m },
                    { 35, "Description for product 35", "Product 35", 197m },
                    { 34, "Description for product 34", "Product 34", 282m },
                    { 33, "Description for product 33", "Product 33", 149m },
                    { 32, "Description for product 32", "Product 32", 551m },
                    { 31, "Description for product 31", "Product 31", 568m },
                    { 30, "Description for product 30", "Product 30", 924m },
                    { 29, "Description for product 29", "Product 29", 813m },
                    { 28, "Description for product 28", "Product 28", 515m },
                    { 27, "Description for product 27", "Product 27", 878m },
                    { 37, "Description for product 37", "Product 37", 928m },
                    { 100, "Description for product 100", "Product 100", 335m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 79 },
                    { 73, 73, 3 },
                    { 72, 72, 10 },
                    { 71, 71, 27 },
                    { 70, 70, 26 },
                    { 69, 69, 16 },
                    { 68, 68, 5 },
                    { 67, 67, 9 },
                    { 66, 66, 94 },
                    { 65, 65, 14 },
                    { 64, 64, 11 },
                    { 63, 63, 4 },
                    { 62, 62, 67 },
                    { 61, 61, 83 },
                    { 60, 60, 91 },
                    { 59, 59, 62 },
                    { 58, 58, 88 },
                    { 57, 57, 53 },
                    { 56, 56, 78 },
                    { 55, 55, 70 },
                    { 54, 54, 85 },
                    { 53, 53, 25 },
                    { 74, 74, 2 },
                    { 52, 52, 1 },
                    { 75, 75, 18 },
                    { 77, 77, 63 },
                    { 98, 98, 25 },
                    { 97, 97, 45 },
                    { 96, 96, 19 },
                    { 95, 95, 24 },
                    { 94, 94, 67 },
                    { 93, 93, 24 },
                    { 92, 92, 67 },
                    { 91, 91, 56 },
                    { 90, 90, 38 },
                    { 89, 89, 89 },
                    { 88, 88, 48 },
                    { 87, 87, 19 },
                    { 86, 86, 93 },
                    { 85, 85, 67 },
                    { 84, 84, 59 },
                    { 83, 83, 12 },
                    { 82, 82, 88 },
                    { 81, 81, 40 },
                    { 80, 80, 98 },
                    { 79, 79, 76 },
                    { 78, 78, 99 },
                    { 76, 76, 10 },
                    { 51, 51, 27 },
                    { 50, 50, 70 },
                    { 49, 49, 24 },
                    { 22, 22, 0 },
                    { 21, 21, 60 },
                    { 20, 20, 5 },
                    { 19, 19, 10 },
                    { 18, 18, 62 },
                    { 17, 17, 97 },
                    { 16, 16, 83 },
                    { 15, 15, 35 },
                    { 14, 14, 33 },
                    { 13, 13, 64 },
                    { 12, 12, 43 },
                    { 11, 11, 39 },
                    { 10, 10, 9 },
                    { 9, 9, 6 },
                    { 8, 8, 35 },
                    { 7, 7, 3 },
                    { 6, 6, 70 },
                    { 5, 5, 13 },
                    { 4, 4, 73 },
                    { 3, 3, 88 },
                    { 2, 2, 54 },
                    { 23, 23, 0 },
                    { 24, 24, 0 },
                    { 25, 25, 11 },
                    { 26, 26, 22 },
                    { 48, 48, 98 },
                    { 47, 47, 82 },
                    { 46, 46, 12 },
                    { 45, 45, 23 },
                    { 44, 44, 1 },
                    { 43, 43, 37 },
                    { 42, 42, 49 },
                    { 41, 41, 86 },
                    { 40, 40, 7 },
                    { 39, 39, 58 },
                    { 99, 99, 86 },
                    { 38, 38, 86 },
                    { 36, 36, 9 },
                    { 35, 35, 17 },
                    { 34, 34, 59 },
                    { 33, 33, 34 },
                    { 32, 32, 44 },
                    { 31, 31, 95 },
                    { 30, 30, 95 },
                    { 29, 29, 30 },
                    { 28, 28, 36 },
                    { 27, 27, 87 },
                    { 37, 37, 55 },
                    { 100, 100, 29 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductInStockId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductInStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
