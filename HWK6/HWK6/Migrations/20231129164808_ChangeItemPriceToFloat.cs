using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HWK6.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemPriceToFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dlvy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DlvyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dlvy__3214EC27F447C463", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Station__3214EC270425E54F", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Store__3214EC27AFCE7937", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Item__3214EC2789BCEAFA", x => x.ID);
                    table.ForeignKey(
                        name: "Item_Fk_Station",
                        column: x => x.StationID,
                        principalTable: "Station",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    DlvyID = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__3214EC270CEBB029", x => x.ID);
                    table.ForeignKey(
                        name: "Order_Fk_Dlvy",
                        column: x => x.DlvyID,
                        principalTable: "Dlvy",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Order_Fk_Store",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ItemID = table.Column<int>(type: "int", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__3214EC27AAF36D16", x => x.ID);
                    table.ForeignKey(
                        name: "OrderItem_Fk_Item",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "OrderItem_Fk_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_StationID",
                table: "Item",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DlvyID",
                table: "Order",
                column: "DlvyID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StoreID",
                table: "Order",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemID",
                table: "OrderItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropTable(
                name: "Dlvy");

            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
