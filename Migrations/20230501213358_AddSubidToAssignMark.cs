using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSuccessPrediction.Migrations
{
    public partial class AddSubidToAssignMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "AssignmentMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentMarks_SubjectId",
                table: "AssignmentMarks",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentMarks_Subjects_SubjectId",
                table: "AssignmentMarks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentMarks_Subjects_SubjectId",
                table: "AssignmentMarks");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentMarks_SubjectId",
                table: "AssignmentMarks");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "AssignmentMarks");
        }
    }
}
