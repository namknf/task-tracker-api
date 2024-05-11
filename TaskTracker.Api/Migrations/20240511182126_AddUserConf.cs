using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Api.Migrations
{
    public partial class AddUserConf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailCode", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoId", "Position", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "603dadb8-e3c0-4328-b362-6348be30f147", 0, "5d3d70eb-2320-497f-b1df-796277e5e9ce", "anya.makeeva.2004@mail.ru", null, false, "Ann", "Makeeva", false, null, null, null, "AQAAAAEAACcQAAAAEIs6p1wbul6Fh90ZwkfH1EkZTgd3os6veZKM7qTkZOR7rL23c074hkmJiVKQfWCprQ==", null, false, null, "Frontend Developer", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "30cac050-b31f-4df0-bc73-df9356bf4949", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailCode", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoId", "Position", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "73e58b1b-6bc7-4484-9c12-807d120cae36", 0, "f3cf4dd5-ff6b-4b7d-80c1-5dd449a6a4d0", "life_paradishe@mail.ru", null, false, "Ruslan", "Palytin", false, null, null, null, "AQAAAAEAACcQAAAAECdLLLF5OblHM/TVOv+Tnu2VFd3uwjHoJ4hHeCj8IjN1vyhK55Zo84hjIlPt5AyObA==", null, false, null, "IOS Developer", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5c12ef75-bff3-4939-9366-c0e2e088e8b0", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailCode", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoId", "Position", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "91e81946-7879-4790-9e28-f2eb0e6d796c", 0, "e72a917c-0974-4cff-aad8-b33a040f78ea", "9093264418es@gmail.com", null, false, "Anastasia", "Malkina", false, null, null, null, "AQAAAAEAACcQAAAAEOxw9x+zJWfgbv1Sg0msY9JRoJZ6850nYI2nk1HjOYKxr0Ph+3eSGnXmW4bVBUwe1A==", null, false, null, "Backend Developer", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "052ba646-69d2-42b2-ad6b-e60d6a382a5e", false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "603dadb8-e3c0-4328-b362-6348be30f147");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73e58b1b-6bc7-4484-9c12-807d120cae36");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91e81946-7879-4790-9e28-f2eb0e6d796c");
        }
    }
}
