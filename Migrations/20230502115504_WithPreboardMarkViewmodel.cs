using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSuccessPrediction.Migrations
{
    public partial class WithPreboardMarkViewmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreboardMark_Students_StudentId",
                table: "PreboardMark");

            migrationBuilder.DropForeignKey(
                name: "FK_PreboardSubjectMarks_PreboardMark_PreboardMarkId",
                table: "PreboardSubjectMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreboardMark",
                table: "PreboardMark");

            migrationBuilder.RenameTable(
                name: "PreboardMark",
                newName: "PreboardMarks");

            migrationBuilder.RenameIndex(
                name: "IX_PreboardMark_StudentId",
                table: "PreboardMarks",
                newName: "IX_PreboardMarks_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreboardMarks",
                table: "PreboardMarks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreboardMarks_Students_StudentId",
                table: "PreboardMarks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreboardSubjectMarks_PreboardMarks_PreboardMarkId",
                table: "PreboardSubjectMarks",
                column: "PreboardMarkId",
                principalTable: "PreboardMarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreboardMarks_Students_StudentId",
                table: "PreboardMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_PreboardSubjectMarks_PreboardMarks_PreboardMarkId",
                table: "PreboardSubjectMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreboardMarks",
                table: "PreboardMarks");

            migrationBuilder.RenameTable(
                name: "PreboardMarks",
                newName: "PreboardMark");

            migrationBuilder.RenameIndex(
                name: "IX_PreboardMarks_StudentId",
                table: "PreboardMark",
                newName: "IX_PreboardMark_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreboardMark",
                table: "PreboardMark",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreboardMark_Students_StudentId",
                table: "PreboardMark",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreboardSubjectMarks_PreboardMark_PreboardMarkId",
                table: "PreboardSubjectMarks",
                column: "PreboardMarkId",
                principalTable: "PreboardMark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
