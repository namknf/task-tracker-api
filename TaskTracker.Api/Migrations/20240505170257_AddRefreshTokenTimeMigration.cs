using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Api.Migrations
{
    public partial class AddRefreshTokenTimeMigration : Migration
    {
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
                values: new object[] { new Guid("2851e08b-3776-4e0a-b7c8-fa322b6b9287"), "To do" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("85e89f1f-0a57-4e4a-9581-57c382c87140"), "Closed" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("b8e6b6d6-cf8d-48b6-9baa-ce4669bbe182"), "Frozen" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[] { new Guid("defd5ea6-ee56-47c3-82d9-6954efadef60"), "In Progress" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("57a711db-b96b-4365-b877-8c042f03c43c"), "High" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("70c8795f-300a-4735-a8a9-9565aabdff40"), "Low" });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "PriorityName" },
                values: new object[] { new Guid("dd9ced36-3529-4db2-8bf8-d523c864ed54"), "Medium" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2851e08b-3776-4e0a-b7c8-fa322b6b9287"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("85e89f1f-0a57-4e4a-9581-57c382c87140"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b8e6b6d6-cf8d-48b6-9baa-ce4669bbe182"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("defd5ea6-ee56-47c3-82d9-6954efadef60"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("57a711db-b96b-4365-b877-8c042f03c43c"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("70c8795f-300a-4735-a8a9-9565aabdff40"));

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: new Guid("dd9ced36-3529-4db2-8bf8-d523c864ed54"));

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
