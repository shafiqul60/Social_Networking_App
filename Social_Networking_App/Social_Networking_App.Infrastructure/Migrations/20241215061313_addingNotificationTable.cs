using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Networking_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendNotifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromRequest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToRequest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotiMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendNotifications", x => x.NotificationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendNotifications");
        }
    }
}
