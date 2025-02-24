using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_CreatedBy",
                table: "FriendInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_FriendsUsername",
                table: "FriendInvite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendInvite",
                table: "FriendInvite");

            migrationBuilder.RenameTable(
                name: "FriendInvite",
                newName: "FriendInvites");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvite_FriendsUsername",
                table: "FriendInvites",
                newName: "IX_FriendInvites_FriendsUsername");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvite_CreatedBy",
                table: "FriendInvites",
                newName: "IX_FriendInvites_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendInvites",
                table: "FriendInvites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvites_AspNetUsers_CreatedBy",
                table: "FriendInvites",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvites_AspNetUsers_FriendsUsername",
                table: "FriendInvites",
                column: "FriendsUsername",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvites_AspNetUsers_CreatedBy",
                table: "FriendInvites");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvites_AspNetUsers_FriendsUsername",
                table: "FriendInvites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendInvites",
                table: "FriendInvites");

            migrationBuilder.RenameTable(
                name: "FriendInvites",
                newName: "FriendInvite");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvites_FriendsUsername",
                table: "FriendInvite",
                newName: "IX_FriendInvite_FriendsUsername");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvites_CreatedBy",
                table: "FriendInvite",
                newName: "IX_FriendInvite_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendInvite",
                table: "FriendInvite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_CreatedBy",
                table: "FriendInvite",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_FriendsUsername",
                table: "FriendInvite",
                column: "FriendsUsername",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
