using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.Persistence.Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Order");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "Order",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Order",
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "OrderId", "ProductId", "Quantity", "Total", "UnitPrice" },
                values: new object[,]
                {
                    { 20, null, 20, 20, 20m, 20m },
                    { 2, null, 2, 2, 2m, 2m },
                    { 3, null, 3, 3, 3m, 3m },
                    { 4, null, 4, 4, 4m, 4m },
                    { 5, null, 5, 5, 5m, 5m },
                    { 6, null, 6, 6, 6m, 6m },
                    { 7, null, 7, 7, 7m, 7m },
                    { 8, null, 8, 8, 8m, 8m },
                    { 9, null, 9, 9, 9m, 9m },
                    { 10, null, 10, 10, 10m, 10m },
                    { 11, null, 11, 11, 11m, 11m },
                    { 12, null, 12, 12, 12m, 12m },
                    { 13, null, 13, 13, 13m, 13m },
                    { 14, null, 14, 14, 14m, 14m },
                    { 15, null, 15, 15, 15m, 15m },
                    { 16, null, 16, 16, 16m, 16m },
                    { 17, null, 17, 17, 17m, 17m },
                    { 18, null, 18, 18, 18m, 18m },
                    { 1, null, 1, 1, 1m, 1m },
                    { 19, null, 19, 19, 19m, 19m }
                });

            migrationBuilder.InsertData(
                schema: "Order",
                table: "Orders",
                columns: new[] { "OrderId", "ClientId", "CreatedAt", "PaymentType", "Status", "Total" },
                values: new object[,]
                {
                    { 20, 20, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1294), 1, 1, 100m },
                    { 18, 18, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1291), 1, 1, 100m },
                    { 2, 2, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1247), 1, 1, 100m },
                    { 3, 3, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1268), 1, 1, 100m },
                    { 4, 4, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1270), 1, 1, 100m },
                    { 5, 5, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1272), 1, 1, 100m },
                    { 6, 6, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1275), 1, 1, 100m },
                    { 7, 7, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1277), 1, 1, 100m },
                    { 8, 8, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1278), 1, 1, 100m },
                    { 19, 19, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1293), 1, 1, 100m },
                    { 9, 9, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1279), 1, 1, 100m },
                    { 11, 11, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1282), 1, 1, 100m },
                    { 12, 12, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1284), 1, 1, 100m },
                    { 13, 13, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1285), 1, 1, 100m },
                    { 14, 14, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1286), 1, 1, 100m },
                    { 15, 15, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1287), 1, 1, 100m },
                    { 16, 16, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1288), 1, 1, 100m },
                    { 17, 17, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1289), 1, 1, 100m },
                    { 10, 10, new DateTime(2023, 5, 3, 16, 51, 29, 631, DateTimeKind.Local).AddTicks(1281), 1, 1, 100m },
                    { 1, 1, new DateTime(2023, 5, 3, 16, 51, 29, 630, DateTimeKind.Local).AddTicks(3981), 1, 1, 100m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderDetailId",
                schema: "Order",
                table: "OrderDetails",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "Order",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderId",
                schema: "Order",
                table: "Orders",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Order");
        }
    }
}
