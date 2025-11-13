using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class FixExerciseCategoryCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseCategories_ExerciseCategoryId",
                table: "Exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseCategories_ExerciseCategoryId",
                table: "Exercises",
                column: "ExerciseCategoryId",
                principalTable: "ExerciseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseCategories_ExerciseCategoryId",
                table: "Exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseCategories_ExerciseCategoryId",
                table: "Exercises",
                column: "ExerciseCategoryId",
                principalTable: "ExerciseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
