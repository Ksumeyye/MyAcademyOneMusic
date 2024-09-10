using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneMusic.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_aspuser_album_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Albums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AppUserId",
                table: "Albums",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_AppUserId",
                table: "Albums",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_AppUserId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_AppUserId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Albums");
        }
    }
}
