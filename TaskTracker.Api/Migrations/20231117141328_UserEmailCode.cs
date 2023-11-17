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

            migrationBuilder.AlterColumn<string>(
                name: "EmailCode",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("265148d3-e438-4104-96dc-43c8c6dc0489"), "Closed" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("d31c4737-c1d3-4a1f-b50e-a1694c1219ef"), "Frozen" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("ebbe0afd-dcac-4a65-a405-a14841412f78"), "To do" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("f89a40b9-3d02-4a7e-aabe-5fa17b582fd0"), "In Progress" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("3f8eae8e-ec2f-4151-8221-bdade0e16e71"), "Low" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("4b3ef4a6-aabf-40f0-b3e8-c82936c43777"), "High" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("725e8393-ee68-45f9-a509-900b3851039e"), "Medium" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("265148d3-e438-4104-96dc-43c8c6dc0489"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("d31c4737-c1d3-4a1f-b50e-a1694c1219ef"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("ebbe0afd-dcac-4a65-a405-a14841412f78"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("f89a40b9-3d02-4a7e-aabe-5fa17b582fd0"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("3f8eae8e-ec2f-4151-8221-bdade0e16e71"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("4b3ef4a6-aabf-40f0-b3e8-c82936c43777"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("725e8393-ee68-45f9-a509-900b3851039e"));

            migrationBuilder.AlterColumn<string>(
                name: "EmailCode",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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
    }
}
