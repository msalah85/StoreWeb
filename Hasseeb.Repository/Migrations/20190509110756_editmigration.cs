using Microsoft.EntityFrameworkCore.Migrations;

namespace Hasseeb.Repository.Migrations
{
    public partial class editmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountNatures_AccountNatureID",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountNatureID",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNatureID",
                table: "Accounts",
                column: "AccountNatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountNatures_AccountNatureID",
                table: "Accounts",
                column: "AccountNatureID",
                principalTable: "AccountNatures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
