using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_Application.Migrations
{
    /// <inheritdoc />
    public partial class newschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isOnline",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "SingleChatMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOnline",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "SingleChatMessage");
        }
    }
}
