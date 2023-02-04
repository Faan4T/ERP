using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class CoursesinStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "coursesId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_coursesId",
                table: "Students",
                column: "coursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_coursesId",
                table: "Students",
                column: "coursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_coursesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_coursesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "coursesId",
                table: "Students");
        }
    }
}
