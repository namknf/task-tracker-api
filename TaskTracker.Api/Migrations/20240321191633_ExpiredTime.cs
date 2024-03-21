using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class ExpiredTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("26153ca4-13a3-4058-aafa-08ea8993912b"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("81f6b1c1-7ce2-4377-98c3-924c8c7caae2"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("9c2626b0-0241-4a65-b237-b151adcadf60"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b112add8-7a61-4fe3-9f22-b31b18562b96"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("13f78a9a-4647-43a7-8884-5375f6f482ae"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("163316c2-3a1d-4fcd-9d2a-f98c3dd0ea11"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("6315d9d0-b54d-4811-9965-3eab2b3ccf92"));

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("767f5b82-4a10-4f16-b937-392acbba7c92"), "Closed" },
                    { new Guid("7c044558-cce7-40f7-9214-baa2acecb2ca"), "To do" },
                    { new Guid("99a97551-9983-4d69-8cf9-d5a15e0a4d92"), "Frozen" },
                    { new Guid("a8ad7521-8ca7-4fb2-8bfa-7562d25120aa"), "In Progress" }
                });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[,]
                {
                    { new Guid("aa625601-74a8-4bdd-81ec-3dba7011de8c"), "Low" },
                    { new Guid("b2e66f78-afe2-4a20-8c0a-4cea167aef5a"), "Medium" },
                    { new Guid("d89b6183-4180-45e1-8b80-dd7e2f1f7cbf"), "High" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("767f5b82-4a10-4f16-b937-392acbba7c92"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("7c044558-cce7-40f7-9214-baa2acecb2ca"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("99a97551-9983-4d69-8cf9-d5a15e0a4d92"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("a8ad7521-8ca7-4fb2-8bfa-7562d25120aa"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("aa625601-74a8-4bdd-81ec-3dba7011de8c"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("b2e66f78-afe2-4a20-8c0a-4cea167aef5a"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("d89b6183-4180-45e1-8b80-dd7e2f1f7cbf"));

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("26153ca4-13a3-4058-aafa-08ea8993912b"), "To do" },
                    { new Guid("81f6b1c1-7ce2-4377-98c3-924c8c7caae2"), "Closed" },
                    { new Guid("9c2626b0-0241-4a65-b237-b151adcadf60"), "Frozen" },
                    { new Guid("b112add8-7a61-4fe3-9f22-b31b18562b96"), "In Progress" }
                });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[,]
                {
                    { new Guid("13f78a9a-4647-43a7-8884-5375f6f482ae"), "Medium" },
                    { new Guid("163316c2-3a1d-4fcd-9d2a-f98c3dd0ea11"), "High" },
                    { new Guid("6315d9d0-b54d-4811-9965-3eab2b3ccf92"), "Low" }
                });
        }
    }
}
