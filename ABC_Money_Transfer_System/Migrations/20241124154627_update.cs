using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC_Money_Transfer_System.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UsersId",
                table: "Accounts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UsersId",
                table: "Accounts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UsersId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UsersId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Accounts");
        }
    }
}
