using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_APP_PROJECT.Migrations
{
    public partial class OrderDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodShopName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    riderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    callRider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    callUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
