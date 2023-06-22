using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemasWeb01.Migrations
{
    /// <inheritdoc />
    public partial class addPropertyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeUser",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeUser",
                table: "AspNetUsers");
        }
    }
}
