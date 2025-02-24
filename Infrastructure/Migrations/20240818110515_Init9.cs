using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_FriendUnique",
                table: "FriendInvite");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FriendUnique",
                table: "FriendInvite",
                newName: "FriendsUsername");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvite_FriendUnique",
                table: "FriendInvite",
                newName: "IX_FriendInvite_FriendsUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_FriendsUsername",
                table: "FriendInvite",
                column: "FriendsUsername",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_FriendsUsername",
                table: "FriendInvite");

            migrationBuilder.RenameColumn(
                name: "FriendsUsername",
                table: "FriendInvite",
                newName: "FriendUnique");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvite_FriendsUsername",
                table: "FriendInvite",
                newName: "IX_FriendInvite_FriendUnique");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_FriendUnique",
                table: "FriendInvite",
                column: "FriendUnique",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
