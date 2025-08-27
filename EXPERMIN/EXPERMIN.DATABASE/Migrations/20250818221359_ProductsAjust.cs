using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXPERMIN.DATABASE.Migrations
{
    /// <inheritdoc />
    public partial class ProductsAjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SequenceOrder",
                table: "Banners",
                newName: "Order");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Banners",
                newName: "SequenceOrder");
        }
    }
}
