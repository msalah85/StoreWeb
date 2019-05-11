using Microsoft.EntityFrameworkCore.Migrations;

namespace Hasseeb.Repository.Migrations
{
    public partial class editmigrationnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentAccountID",
                table: "Accounts",
                newName: "ParentAccount");

            migrationBuilder.RenameColumn(
                name: "AccountNatureID",
                table: "Accounts",
                newName: "AccountNature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentAccount",
                table: "Accounts",
                newName: "ParentAccountID");

            migrationBuilder.RenameColumn(
                name: "AccountNature",
                table: "Accounts",
                newName: "AccountNatureID");
        }
    }
}
