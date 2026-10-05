using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlignWithErd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectTeacher_Class_ClassId",
                table: "ClassSubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectTeacher_Subject_SubjectId",
                table: "ClassSubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Class_ClassId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedule_Class_ClassId",
                table: "RoomSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedule_Subject_SubjectId",
                table: "RoomSchedule");

            migrationBuilder.DropIndex(
                name: "IX_RoomSchedule_ClassId_Date_PeriodId",
                table: "RoomSchedule");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RoomSchedule_Class",
                table: "RoomSchedule");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_ClassId",
                table: "Enrollment");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Enrollment_DateRange",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherAssignment_ClassId",
                table: "ClassTeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjectTeacher_ClassId",
                table: "ClassSubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjectTeacher_SubjectId",
                table: "ClassSubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolYearId_Name",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "Booking",
                table: "RoomSchedule");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "RoomSchedule");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassSubjectTeacher");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Subject",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RoomSchedule",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "RoomSchedule",
                newName: "ClassSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomSchedule_SubjectId",
                table: "RoomSchedule",
                newName: "IX_RoomSchedule_ClassSubjectId");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Room",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Enrollment",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "ClassTeacherAssignment",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "ClassSubjectTeacher",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "ClassSubjectTeacher",
                newName: "ClassSubjectId");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Class",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Subject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Subject",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<byte>(
                name: "BookingType",
                table: "RoomSchedule",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "RoomSchedule",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Enrollment",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "ClassTeacherAssignment",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "ClassTeacherAssignment",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Class",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Class_Id_SchoolYearId",
                table: "Class",
                columns: new[] { "Id", "SchoolYearId" });

            migrationBuilder.CreateTable(
                name: "ClassSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSubject_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SchoolId_Name",
                table: "Subject",
                columns: new[] { "SchoolId", "Name" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoomSchedule_Lesson_ClassSubject",
                table: "RoomSchedule",
                sql: "[BookingType] <> 1 OR [ClassSubjectId] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoomSchedule_NonLesson_NoTopic",
                table: "RoomSchedule",
                sql: "[BookingType] = 1 OR [Topic] IS NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoomSchedule_NonLesson_Title",
                table: "RoomSchedule",
                sql: "[BookingType] = 1 OR [Title] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Room_SchoolId_Name",
                table: "Room",
                columns: new[] { "SchoolId", "Name" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_SchoolId_SchoolYearId_Number",
                table: "Periods",
                columns: new[] { "SchoolId", "SchoolYearId", "Number" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ClassId_SchoolYearId",
                table: "Enrollment",
                columns: new[] { "ClassId", "SchoolYearId" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId_SchoolYearId",
                table: "Enrollment",
                columns: new[] { "StudentId", "SchoolYearId" },
                unique: true,
                filter: "[EndDate] IS NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Enrollment_DateRange",
                table: "Enrollment",
                sql: "[EndDate] IS NULL OR [EndDate] > [StartDate]");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacherAssignment_ClassId",
                table: "ClassTeacherAssignment",
                column: "ClassId",
                unique: true,
                filter: "[IsHomeRoomTeacher] = 1 AND [EndDate] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_ClassSubjectId",
                table: "ClassSubjectTeacher",
                column: "ClassSubjectId",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolYearId_Name",
                table: "Class",
                columns: new[] { "SchoolYearId", "Name" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_ClassId_SubjectId",
                table: "ClassSubject",
                columns: new[] { "ClassId", "SubjectId" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_SubjectId",
                table: "ClassSubject",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectTeacher_ClassSubject_ClassSubjectId",
                table: "ClassSubjectTeacher",
                column: "ClassSubjectId",
                principalTable: "ClassSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Class_ClassId_SchoolYearId",
                table: "Enrollment",
                columns: new[] { "ClassId", "SchoolYearId" },
                principalTable: "Class",
                principalColumns: new[] { "Id", "SchoolYearId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedule_ClassSubject_ClassSubjectId",
                table: "RoomSchedule",
                column: "ClassSubjectId",
                principalTable: "ClassSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectTeacher_ClassSubject_ClassSubjectId",
                table: "ClassSubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Class_ClassId_SchoolYearId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedule_ClassSubject_ClassSubjectId",
                table: "RoomSchedule");

            migrationBuilder.DropTable(
                name: "ClassSubject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SchoolId_Name",
                table: "Subject");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RoomSchedule_Lesson_ClassSubject",
                table: "RoomSchedule");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RoomSchedule_NonLesson_NoTopic",
                table: "RoomSchedule");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RoomSchedule_NonLesson_Title",
                table: "RoomSchedule");

            migrationBuilder.DropIndex(
                name: "IX_Room_SchoolId_Name",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Periods_SchoolId_SchoolYearId_Number",
                table: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_ClassId_SchoolYearId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentId_SchoolYearId",
                table: "Enrollment");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Enrollment_DateRange",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherAssignment_ClassId",
                table: "ClassTeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjectTeacher_ClassSubjectId",
                table: "ClassSubjectTeacher");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Class_Id_SchoolYearId",
                table: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolYearId_Name",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "BookingType",
                table: "RoomSchedule");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "RoomSchedule");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Class");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Subject",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "RoomSchedule",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ClassSubjectId",
                table: "RoomSchedule",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomSchedule_ClassSubjectId",
                table: "RoomSchedule",
                newName: "IX_RoomSchedule_SubjectId");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Room",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Enrollment",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "ClassTeacherAssignment",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "ClassSubjectTeacher",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ClassSubjectId",
                table: "ClassSubjectTeacher",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Class",
                newName: "ModifiedDate");

            migrationBuilder.AddColumn<string>(
                name: "Booking",
                table: "RoomSchedule",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "RoomSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Enrollment",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ClassTeacherAssignment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ClassTeacherAssignment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "ClassSubjectTeacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_ClassId_Date_PeriodId",
                table: "RoomSchedule",
                columns: new[] { "ClassId", "Date", "PeriodId" },
                unique: true,
                filter: "[IsActive] = 1 AND [ClassId] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoomSchedule_Class",
                table: "RoomSchedule",
                sql: "([Booking] = 'Class' AND [ClassId] IS NOT NULL) OR ([Booking] <> 'Class' AND [ClassId] IS NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ClassId",
                table: "Enrollment",
                column: "ClassId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Enrollment_DateRange",
                table: "Enrollment",
                sql: "[EndDate] > [StartDate]");

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

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolYearId_Name",
                table: "Class",
                columns: new[] { "SchoolYearId", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectTeacher_Class_ClassId",
                table: "ClassSubjectTeacher",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectTeacher_Subject_SubjectId",
                table: "ClassSubjectTeacher",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Class_ClassId",
                table: "Enrollment",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedule_Class_ClassId",
                table: "RoomSchedule",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedule_Subject_SubjectId",
                table: "RoomSchedule",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");
        }
    }
}
