using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListingDemo.API.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48c227ff-1081-43df-9b87-346bbe9938cd", "de3b7ec9-6cf0-41b9-910e-0341e8079945", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a461b1f5-4e08-40e1-bab2-ea21e900114b", "2ac42cde-5311-4734-acb1-6d6fe08001f7", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48c227ff-1081-43df-9b87-346bbe9938cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a461b1f5-4e08-40e1-bab2-ea21e900114b");
        }
    }
}
