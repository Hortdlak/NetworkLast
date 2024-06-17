using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Network.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message_data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_sent = table.Column<bool>(type: "bit", nullable: false),
                    UserToId = table.Column<int>(type: "int", nullable: true),
                    UserFromId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("message_pk", x => x.id);
                    table.ForeignKey(
                        name: "message_from_user_fk",
                        column: x => x.UserFromId,
                        principalTable: "users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "message_to_user_fk",
                        column: x => x.UserToId,
                        principalTable: "users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserFromId",
                table: "messages",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserToId",
                table: "messages",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_users_FullName",
                table: "users",
                column: "FullName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
