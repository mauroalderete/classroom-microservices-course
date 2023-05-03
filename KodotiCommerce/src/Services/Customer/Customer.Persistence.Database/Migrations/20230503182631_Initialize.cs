using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Persistence.Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Customer",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "Customer",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Client 1" },
                    { 18, "Client 18" },
                    { 17, "Client 17" },
                    { 16, "Client 16" },
                    { 15, "Client 15" },
                    { 14, "Client 14" },
                    { 13, "Client 13" },
                    { 12, "Client 12" },
                    { 11, "Client 11" },
                    { 10, "Client 10" },
                    { 9, "Client 9" },
                    { 8, "Client 8" },
                    { 7, "Client 7" },
                    { 6, "Client 6" },
                    { 5, "Client 5" },
                    { 4, "Client 4" },
                    { 3, "Client 3" },
                    { 2, "Client 2" },
                    { 19, "Client 19" },
                    { 20, "Client 20" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                schema: "Customer",
                table: "Clients",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Customer");
        }
    }
}
