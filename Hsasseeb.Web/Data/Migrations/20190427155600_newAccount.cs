using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hsasseeb.Web.Data.Migrations
{
    public partial class newAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountNatures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNatureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNatures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNatureID = table.Column<int>(nullable: true),
                    ParentAccountID = table.Column<int>(nullable: true),
                    AccountSerial = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    AccountDesc = table.Column<string>(nullable: true),
                    GroupOrder = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountNatures_AccountNatureID",
                        column: x => x.AccountNatureID,
                        principalTable: "AccountNatures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNatureID",
                table: "Accounts",
                column: "AccountNatureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountNatures");
        }
    }
}
