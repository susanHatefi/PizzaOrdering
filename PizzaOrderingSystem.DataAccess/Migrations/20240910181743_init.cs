using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaOrderingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(22,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(22,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(22,2)", nullable: false, defaultValue: 0m),
                    Discount = table.Column<short>(type: "smallint", nullable: true),
                    TotalToppings = table.Column<short>(type: "smallint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(22,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(22,2)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ToppingUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToppingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<short>(type: "smallint", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToppingUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToppingUnit_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem_Topping",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToppingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem_Topping", x => new { x.OrderItemId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_OrderItem_Topping_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Topping_Topping_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Topping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("5ca0be9b-70f8-4e4f-9a61-894efbbf429c"), 8m, "Large" },
                    { new Guid("67fbc163-4f48-4bfc-9855-e4b14197e29a"), 5m, "Small" },
                    { new Guid("6f604a8c-18e9-41a2-84c2-90aa906cc60d"), 7m, "Medium" },
                    { new Guid("e42c97ee-92c8-4b44-a9a5-fde9e7848f98"), 9m, "ExtraLarge" }
                });

            migrationBuilder.InsertData(
                table: "Promotion",
                columns: new[] { "Id", "Active", "Description", "Discount", "Name", "ProductSize", "Quantity", "TotalToppings" },
                values: new object[] { new Guid("63794e3d-63b6-4e16-8c11-1145feb35638"), true, null, (short)50, "Offer3", "Large", 0, (short)4 });

            migrationBuilder.InsertData(
                table: "Promotion",
                columns: new[] { "Id", "Active", "Description", "Discount", "Name", "Price", "ProductSize", "Quantity", "TotalToppings" },
                values: new object[,]
                {
                    { new Guid("7592dd68-f457-405f-b8d9-c7a89c740a23"), true, null, null, "Offer1", 5m, "Medium", 0, (short)2 },
                    { new Guid("c9721317-1e81-49a7-89b6-4d268229a2ee"), true, null, null, "Offer2", 9m, "Medium", 2, (short)4 }
                });

            migrationBuilder.InsertData(
                table: "Topping",
                columns: new[] { "Id", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("28b38e6a-3b79-4c67-8326-b00f0a975bf3"), "NonVeg", "Sausage", 1m },
                    { new Guid("2c92ba36-89e6-4851-b405-72d8801f01fd"), "NonVeg", "Barbecue Chicken", 3m },
                    { new Guid("5a690aec-cead-436e-8432-4e52eede0008"), "NonVeg", "Pepperoni", 2m },
                    { new Guid("69b74b47-7113-4821-b5e0-47feb1d7e07a"), "Veg", "Mushrooms", 1.2m },
                    { new Guid("852da74f-2832-4f92-acb2-dc65567954b5"), "Veg", "Tomatoes", 1m },
                    { new Guid("8bfdb819-eef3-497b-8389-50b88cdcb6fe"), "Veg", "Pineapple", 0.75m },
                    { new Guid("c83a3a3b-f6ef-4690-a8e1-5f3f2b50bbab"), "Veg", "Onions", 0.5m },
                    { new Guid("e08d5295-0a02-4560-80b1-9d2e4d585ef0"), "Veg", "Bell Pepper", 1m }
                });

            migrationBuilder.InsertData(
                table: "ToppingUnit",
                columns: new[] { "Id", "PromotionId", "ToppingName", "Unit" },
                values: new object[,]
                {
                    { new Guid("1c2be781-d444-4296-9403-0eb0c5202d92"), new Guid("63794e3d-63b6-4e16-8c11-1145feb35638"), "Pepperoni", (short)2 },
                    { new Guid("6098c4b9-8496-4f3b-b07d-41be2064bf34"), new Guid("63794e3d-63b6-4e16-8c11-1145feb35638"), "Barbecue Chicken", (short)2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_PromotionId",
                table: "OrderItem",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Topping_ToppingId",
                table: "OrderItem_Topping",
                column: "ToppingId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Size",
                table: "Product",
                column: "Size",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Name",
                table: "Promotion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topping_Name_Category",
                table: "Topping",
                columns: new[] { "Name", "Category" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToppingUnit_PromotionId",
                table: "ToppingUnit",
                column: "PromotionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem_Topping");

            migrationBuilder.DropTable(
                name: "ToppingUnit");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Topping");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Promotion");
        }
    }
}
