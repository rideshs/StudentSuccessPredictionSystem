using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSuccessPrediction.Migrations
{
    public partial class AttendanceMarksUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "AttendanceMarks");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "AttendanceMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_SubjectId",
                table: "AttendanceMarks",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceMarks_Subjects_SubjectId",
                table: "AttendanceMarks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceMarks_Subjects_SubjectId",
                table: "AttendanceMarks");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceMarks_SubjectId",
                table: "AttendanceMarks");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "AttendanceMarks");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "AttendanceMarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
