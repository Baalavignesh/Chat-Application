using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_Application.Migrations
{
    /// <inheritdoc />
    public partial class ChatApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GroupChat",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_GroupChat_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingleChat",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChat", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_SingleChat_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingleChat_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessage",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    GroupSenderId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessage", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_GroupChatMessage_GroupChat_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupChat",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupChatMessage_User_GroupSenderId",
                        column: x => x.GroupSenderId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_GroupMembers_GroupChat_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupChat",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMembers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyChats",
                columns: table => new
                {
                    MyChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingleChatId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    ChatUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyChats", x => x.MyChatId);
                    table.ForeignKey(
                        name: "FK_MyChats_GroupChat_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupChat",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyChats_SingleChat_SingleChatId",
                        column: x => x.SingleChatId,
                        principalTable: "SingleChat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyChats_User_ChatUserId",
                        column: x => x.ChatUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingleChatMessage",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentChatId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChatMessage", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_SingleChatMessage_SingleChat_ParentChatId",
                        column: x => x.ParentChatId,
                        principalTable: "SingleChat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SingleChatMessage_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChat_AdminId",
                table: "GroupChat",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessage_GroupId",
                table: "GroupChatMessage",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessage_GroupSenderId",
                table: "GroupChatMessage",
                column: "GroupSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChats_ChatUserId",
                table: "MyChats",
                column: "ChatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChats_GroupId",
                table: "MyChats",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChats_SingleChatId",
                table: "MyChats",
                column: "SingleChatId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChat_ReceiverId",
                table: "SingleChat",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChat_SenderId",
                table: "SingleChat",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChatMessage_ParentChatId",
                table: "SingleChatMessage",
                column: "ParentChatId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChatMessage_SenderId",
                table: "SingleChatMessage",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatMessage");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "MyChats");

            migrationBuilder.DropTable(
                name: "SingleChatMessage");

            migrationBuilder.DropTable(
                name: "GroupChat");

            migrationBuilder.DropTable(
                name: "SingleChat");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
