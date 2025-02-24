using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_UserId",
                table: "FriendInvite");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FriendInvite",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvite_UserId",
                table: "FriendInvite",
                newName: "IX_FriendInvite_Username");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_Username",
                table: "FriendInvite",
                column: "Username",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_Username",
                table: "FriendInvite");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "FriendInvite",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvite_Username",
                table: "FriendInvite",
                newName: "IX_FriendInvite_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_UserId",
                table: "FriendInvite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
