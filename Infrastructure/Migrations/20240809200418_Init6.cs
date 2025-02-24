using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserUnique",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Unique",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserUnique",
                table: "Places",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserUnique",
                table: "OrderItems",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserUnique",
                table: "Invites",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserUnique",
                table: "FriendInvite",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserUnique",
                table: "Categories",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Places",
                newName: "UserUnique");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "OrderItems",
                newName: "UserUnique");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Invites",
                newName: "UserUnique");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "FriendInvite",
                newName: "UserUnique");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Categories",
                newName: "UserUnique");

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
                table: "Orders",
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
                name: "Unique",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
