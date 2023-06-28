using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemasWeb01.Migrations
{
    /// <inheritdoc />
    public partial class addTemporalSaleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemporalSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductSizeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporalSales_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TemporalSales_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TemporalSales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSales_ProductId",
                table: "TemporalSales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSales_ProductSizeId",
                table: "TemporalSales",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSales_UserId",
                table: "TemporalSales",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporalSales");
        }
    }
}
