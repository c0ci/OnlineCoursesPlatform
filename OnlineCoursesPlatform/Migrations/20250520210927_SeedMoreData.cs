using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineCoursesPlatform.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "LecturerId", "Title" },
                values: new object[,]
                {
                    { 6, "Уеб", 1, "JavaScript Основи" },
                    { 7, "БД", 1, "Бази от Данни" },
                    { 8, "Програмиране", 1, "ASP.NET MVC" }
                });

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 3,
                column: "CourseId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 4,
                column: "CourseId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 5,
                column: "CourseId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 6,
                column: "CourseId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 7,
                column: "CourseId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 8,
                column: "CourseId",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "LecturerId", "Title" },
                values: new object[,]
                {
                    { 3, "Уеб", 1, "JavaScript Основи" },
                    { 4, "БД", 1, "Бази от Данни" },
                    { 5, "Програмиране", 1, "ASP.NET MVC" }
                });

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 3,
                column: "CourseId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 4,
                column: "CourseId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 5,
                column: "CourseId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 6,
                column: "CourseId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 7,
                column: "CourseId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 8,
                column: "CourseId",
                value: 5);
        }
    }
}
