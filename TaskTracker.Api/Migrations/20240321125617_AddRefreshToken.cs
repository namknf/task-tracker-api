using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Api.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("26153ca4-13a3-4058-aafa-08ea8993912b"), "To do" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("81f6b1c1-7ce2-4377-98c3-924c8c7caae2"), "Closed" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("9c2626b0-0241-4a65-b237-b151adcadf60"), "Frozen" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("b112add8-7a61-4fe3-9f22-b31b18562b96"), "In Progress" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("13f78a9a-4647-43a7-8884-5375f6f482ae"), "Medium" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("163316c2-3a1d-4fcd-9d2a-f98c3dd0ea11"), "High" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("6315d9d0-b54d-4811-9965-3eab2b3ccf92"), "Low" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

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
    }
}
