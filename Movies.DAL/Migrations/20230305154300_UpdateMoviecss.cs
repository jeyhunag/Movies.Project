using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.DAL.Migrations
{
    public partial class UpdateMoviecss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Trends_TrendId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "TrendId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Trends_TrendId",
                table: "Movies",
                column: "TrendId",
                principalTable: "Trends",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Trends_TrendId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "TrendId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Trends_TrendId",
                table: "Movies",
                column: "TrendId",
                principalTable: "Trends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
