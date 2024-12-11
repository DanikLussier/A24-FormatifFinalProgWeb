using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DixCordesServeur.Migrations
{
    /// <inheritdoc />
    public partial class AjoutMessageAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb998303-f644-4d16-966b-21d5b9ae34ff", "AQAAAAIAAYagAAAAEHSsfxCK6mSiS/fn8Amb9gp5xiPBSg9p6H+EN6zvUDPf4P/mse2nyR8c/tdPxJkJhQ==", "b84e50fa-42fc-476d-9b35-5f6b7a247dd8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9554bd8-e8b7-4b24-b902-e5e1251852af", "AQAAAAIAAYagAAAAEB5S98JQvhg4FiFWkxAbASOHTlNKtXYy//msXGLzzxYxFVtJHxJWc6xkNVNtPRRI6A==", "7658e3c3-b813-41b0-8044-d03177e6ebc1" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChannelId", "SentAt", "Text", "UserId" },
                values: new object[] { 6, 1, new DateTime(2024, 5, 11, 18, 51, 2, 0, DateTimeKind.Unspecified), "!!!Message Important!!!", "11111111-1111-1111-1111-111111111111" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "642b0c4f-09f4-4f13-856d-11e478faecd6", "AQAAAAIAAYagAAAAEEQG+7BkhI4dG88ktabGIYSvy54g3TgVFwAcQ2y7Ixmx/EDGMg4dpzFKZdhP39n/hA==", "8d427c0f-528d-4712-8289-928e17d77f12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d38b80d-2fdc-4f94-abb1-76e192c42f23", "AQAAAAIAAYagAAAAELS5TU0kt1Ev2oZZ71dPm14iyEPpc1cPEy0FX162p1hUYElJ4cOtoDo4KLCzxWroZA==", "8c48bc7d-4e79-43c1-9c9d-2eab3377456d" });
        }
    }
}
