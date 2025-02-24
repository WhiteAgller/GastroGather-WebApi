using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_Username",
                table: "FriendInvite");

            migrationBuilder.DropIndex(
                name: "IX_FriendInvite_Username",
                table: "FriendInvite");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "FriendInvite");

            migrationBuilder.CreateIndex(
                name: "IX_FriendInvite_CreatedBy",
                table: "FriendInvite",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_CreatedBy",
                table: "FriendInvite",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvite_AspNetUsers_CreatedBy",
                table: "FriendInvite");

            migrationBuilder.DropIndex(
                name: "IX_FriendInvite_CreatedBy",
                table: "FriendInvite");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "FriendInvite",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FriendInvite_Username",
                table: "FriendInvite",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvite_AspNetUsers_Username",
                table: "FriendInvite",
                column: "Username",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
