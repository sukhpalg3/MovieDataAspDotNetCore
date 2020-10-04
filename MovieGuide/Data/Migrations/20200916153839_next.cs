using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieGuide.Data.Migrations
{
    public partial class next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Movies_MovieID",
                table: "Photos",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
