using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradePilot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddExamScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "GradedCourses");

            migrationBuilder.AlterColumn<decimal>(
                name: "Credits",
                table: "GradedCourses",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "Endterm",
                table: "GradedCourses",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalExam",
                table: "GradedCourses",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Midterm",
                table: "GradedCourses",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endterm",
                table: "GradedCourses");

            migrationBuilder.DropColumn(
                name: "FinalExam",
                table: "GradedCourses");

            migrationBuilder.DropColumn(
                name: "Midterm",
                table: "GradedCourses");

            migrationBuilder.AlterColumn<decimal>(
                name: "Credits",
                table: "GradedCourses",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "GradedCourses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
