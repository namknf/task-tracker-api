using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Api.Migrations
{
    public partial class UserEmailCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("3cc296c3-2cb6-46c2-8546-5ce7b55259b3"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("529b0f8b-5133-4056-bb57-f56b86aebb51"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("5ddf44b1-cf32-4114-93d9-be0dc5240ce6"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("dd10800a-4924-4eb4-b95b-04f694daf9aa"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("5c3e0724-edc9-48bd-8865-9d953189cc96"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("889387b9-0723-4afd-9fd0-67dbce7c23e1"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("8de02021-c1c7-476d-ade7-d8d9be804dd7"));

            migrationBuilder.AddColumn<string>(
                name: "EmailCode",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("0ca44e5b-d462-4431-a850-f33ab1160ff7"), "In Progress" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("47b08b02-74b8-4372-aca4-25c4a0a007d4"), "Frozen" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("7b6b14a7-af20-4c41-a60c-916b98b13a0c"), "To do" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("e9c1f420-912f-4c0a-9b3a-29cb916f5328"), "Closed" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("30bd1e11-e7c0-4f01-b4bd-fb7d0a6753cf"), "Medium" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("e9f1fbc3-463a-41b8-9681-a46721806925"), "Low" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("f7497146-ee82-4792-8615-9199a82cb2be"), "High" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("0ca44e5b-d462-4431-a850-f33ab1160ff7"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("47b08b02-74b8-4372-aca4-25c4a0a007d4"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("7b6b14a7-af20-4c41-a60c-916b98b13a0c"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("e9c1f420-912f-4c0a-9b3a-29cb916f5328"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("30bd1e11-e7c0-4f01-b4bd-fb7d0a6753cf"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("e9f1fbc3-463a-41b8-9681-a46721806925"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("f7497146-ee82-4792-8615-9199a82cb2be"));

            migrationBuilder.DropColumn(
                name: "EmailCode",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("3cc296c3-2cb6-46c2-8546-5ce7b55259b3"), "In Progress" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("529b0f8b-5133-4056-bb57-f56b86aebb51"), "Closed" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("5ddf44b1-cf32-4114-93d9-be0dc5240ce6"), "Frozen" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("dd10800a-4924-4eb4-b95b-04f694daf9aa"), "To do" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("5c3e0724-edc9-48bd-8865-9d953189cc96"), "High" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("889387b9-0723-4afd-9fd0-67dbce7c23e1"), "Low" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("8de02021-c1c7-476d-ade7-d8d9be804dd7"), "Medium" });
        }
    }
}
