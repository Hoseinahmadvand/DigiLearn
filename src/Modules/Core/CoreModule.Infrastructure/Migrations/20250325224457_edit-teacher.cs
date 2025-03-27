using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editteacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Teachers");
        }
    }
}
