using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_SchoolYear_DateRange1",
                table: "SchoolYear");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SchoolYear_DateRange",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolYearId",
                table: "Class");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "SchoolYear",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "SchoolYear",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "SchoolYear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Enrollment",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Enrollment",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Class",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Class",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    SchoolYearId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periods_SchoolYear_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    Booking = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSchedule", x => x.Id);
                    table.CheckConstraint("CK_RoomSchedule_Class", "([Booking] = 'Class' AND [ClassId] IS NOT NULL) OR ([Booking] <> 'Class' AND [ClassId] IS NULL)");
                    table.ForeignKey(
                        name: "FK_RoomSchedule_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoomSchedule_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomSchedule_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_SchoolYear_DateRange",
                table: "SchoolYear",
                sql: "[EndDate] > [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Enrollment_DateRange",
                table: "Enrollment",
                sql: "[EndDate] > [StartDate]");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolYearId_Name",
                table: "Class",
                columns: new[] { "SchoolYearId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Periods_SchoolYearId",
                table: "Periods",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_ClassId_Date_PeriodId",
                table: "RoomSchedule",
                columns: new[] { "ClassId", "Date", "PeriodId" },
                unique: true,
                filter: "[IsActive] = 1 AND [ClassId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_PeriodId",
                table: "RoomSchedule",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_RoomId_Date_PeriodId",
                table: "RoomSchedule",
                columns: new[] { "RoomId", "Date", "PeriodId" },
                unique: true,
                filter: "[IsActive] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomSchedule");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SchoolYear_DateRange",
                table: "SchoolYear");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Enrollment_DateRange",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolYearId_Name",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "SchoolYear");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "SchoolYear",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "SchoolYear",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Class",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Class",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_SchoolYear_DateRange1",
                table: "SchoolYear",
                sql: "[EndDate] > [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SchoolYear_DateRange",
                table: "Enrollment",
                sql: "[EndDate] > [StartDate]");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolYearId",
                table: "Class",
                column: "SchoolYearId");
        }
    }
}
