using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Init489 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13b221e8-a20e-462e-a47d-573ab1963136");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc69da8c-0fad-44a2-9073-88c4f6eeb8c5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47a8ed9a-1ffe-4652-b0f2-5c412bbd5db3", "bce1c032-009b-4151-b466-e75cb29372bd", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e513ef03-2bb6-4985-81e3-4d82366c7ca6", "e5602376-6578-45fd-819f-2603b61aa203", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47a8ed9a-1ffe-4652-b0f2-5c412bbd5db3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e513ef03-2bb6-4985-81e3-4d82366c7ca6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc69da8c-0fad-44a2-9073-88c4f6eeb8c5", "8d3191a9-b8d1-4689-b0e5-8effa694bd1d", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13b221e8-a20e-462e-a47d-573ab1963136", "f82b4930-d24e-42ce-9881-5c00a320cf19", "Administrator", "ADMINISTRATOR" });
        }
    }
}
