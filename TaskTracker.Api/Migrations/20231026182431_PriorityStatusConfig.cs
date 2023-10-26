using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Api.Migrations
{
    public partial class PriorityStatusConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("39d6b3ee-c13e-489f-a58d-0cc1d10a2a7d"), "In Progress" },
                    { new Guid("63ffacc0-98e2-4ea5-9a4c-cfd59a1bdec0"), "Frozen" },
                    { new Guid("8edc4664-0c79-40dc-808d-17b88f80a9e5"), "Closed" },
                    { new Guid("b9e614bb-fb6a-4d87-b6e3-2a46b6caaaa3"), "To do" }
                });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[,]
                {
                    { new Guid("0ede960a-0d7f-4ed8-a4ff-f85634275855"), "Low" },
                    { new Guid("4af2346b-3b9c-4a26-b156-4b0611444861"), "Medium" },
                    { new Guid("a54ebdab-c216-4c84-bd48-048b12ff6563"), "High" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("39d6b3ee-c13e-489f-a58d-0cc1d10a2a7d"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("63ffacc0-98e2-4ea5-9a4c-cfd59a1bdec0"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("8edc4664-0c79-40dc-808d-17b88f80a9e5"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b9e614bb-fb6a-4d87-b6e3-2a46b6caaaa3"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("0ede960a-0d7f-4ed8-a4ff-f85634275855"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("4af2346b-3b9c-4a26-b156-4b0611444861"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("a54ebdab-c216-4c84-bd48-048b12ff6563"));
        }
    }
}
