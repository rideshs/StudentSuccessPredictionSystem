using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSuccessPrediction.Migrations
{
    public partial class PreboardMarkaddedwithOtherRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreboardMark",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalMark = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreboardMark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreboardMark_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreboardSubjectMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkObtained = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PreboardMarkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreboardSubjectMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreboardSubjectMarks_PreboardMark_PreboardMarkId",
                        column: x => x.PreboardMarkId,
                        principalTable: "PreboardMark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreboardSubjectMarks_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreboardMark_StudentId",
                table: "PreboardMark",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PreboardSubjectMarks_PreboardMarkId",
                table: "PreboardSubjectMarks",
                column: "PreboardMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_PreboardSubjectMarks_SubjectId",
                table: "PreboardSubjectMarks",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreboardSubjectMarks");

            migrationBuilder.DropTable(
                name: "PreboardMark");
        }
    }
}
