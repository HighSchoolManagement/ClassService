using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "RoomSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "RoomSchedule",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "ClassTeacherAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassSubjectTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjectTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_SubjectId",
                table: "RoomSchedule",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacherAssignment_ClassId",
                table: "ClassTeacherAssignment",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_ClassId",
                table: "ClassSubjectTeacher",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_SubjectId",
                table: "ClassSubjectTeacher",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacherAssignment_Class_ClassId",
                table: "ClassTeacherAssignment",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedule_Subject_SubjectId",
                table: "RoomSchedule",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherAssignment_Class_ClassId",
                table: "ClassTeacherAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedule_Subject_SubjectId",
                table: "RoomSchedule");

            migrationBuilder.DropTable(
                name: "ClassSubjectTeacher");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_RoomSchedule_SubjectId",
                table: "RoomSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherAssignment_ClassId",
                table: "ClassTeacherAssignment");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "RoomSchedule");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "RoomSchedule");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassTeacherAssignment");
        }
    }
}
