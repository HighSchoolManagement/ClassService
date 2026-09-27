using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolIdToSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Subject");
        }
    }
}
