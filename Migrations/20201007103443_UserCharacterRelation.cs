using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_rzp.Migrations
{
    public partial class UserCharacterRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Characterss",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characterss_UserId",
                table: "Characterss",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characterss_Users_UserId",
                table: "Characterss",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characterss_Users_UserId",
                table: "Characterss");

            migrationBuilder.DropIndex(
                name: "IX_Characterss_UserId",
                table: "Characterss");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Characterss");
        }
    }
}
