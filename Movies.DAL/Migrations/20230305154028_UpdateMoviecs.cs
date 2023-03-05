using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.DAL.Migrations
{
    public partial class UpdateMoviecs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrendId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Trends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trends", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TrendId",
                table: "Movies",
                column: "TrendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Trends_TrendId",
                table: "Movies",
                column: "TrendId",
                principalTable: "Trends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Trends_TrendId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Trends");

            migrationBuilder.DropIndex(
                name: "IX_Movies_TrendId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TrendId",
                table: "Movies");
        }
    }
}
