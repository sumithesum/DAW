using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daw.Migrations
{
    /// <inheritdoc />
    public partial class rev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reviews_ReviewID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewID",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewID",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewID",
                table: "Reviews",
                column: "ReviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reviews_ReviewID",
                table: "Reviews",
                column: "ReviewID",
                principalTable: "Reviews",
                principalColumn: "ID");
        }
    }
}
