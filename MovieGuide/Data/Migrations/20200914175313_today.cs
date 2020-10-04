using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieGuide.Data.Migrations
{
    public partial class today : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MovieID",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MovieID",
                table: "Photos",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MovieID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Photos");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MovieID",
                table: "Photos",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
