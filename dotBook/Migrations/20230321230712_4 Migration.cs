using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotBook.Migrations
{
    /// <inheritdoc />
    public partial class _4Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Customers",
                newName: "JoinDate");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Books",
                newName: "ArrivalDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "Customers",
                newName: "SaleDate");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Books",
                newName: "SaleDate");
        }
    }
}
