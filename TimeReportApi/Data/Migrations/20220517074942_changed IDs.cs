using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeReportApi.Migrations
{
    public partial class changedIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Projects",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                newName: "IX_Projects_customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customers_customerId",
                table: "Projects",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customers_customerId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Projects",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_customerId",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
