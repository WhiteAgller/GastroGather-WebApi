using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Tables",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Places",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Invites",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "FriendInvite",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUnique",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "FriendInvite");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Categories");
        }
    }
}
