using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseTemplates_ExerciseCategories_ExerciseCategoryId",
                        column: x => x.ExerciseCategoryId,
                        principalTable: "ExerciseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ExerciseCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Exercises targeting the pectoral muscles", "Chest" },
                    { 2, "Exercises targeting the back muscles including lats, traps, and rhomboids", "Back" },
                    { 3, "Exercises targeting quadriceps, hamstrings, glutes, and calves", "Legs" },
                    { 4, "Exercises targeting the deltoid muscles", "Shoulders" },
                    { 5, "Exercises targeting biceps and triceps", "Arms" },
                    { 6, "Exercises targeting abdominals and obliques", "Core" },
                    { 7, "Cardiovascular endurance exercises", "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "CaloriesPerServing", "CarbohydratesPerServing", "FatsPerServing", "FoodName", "ProteinPerServing", "ServingSize" },
                values: new object[,]
                {
                    { 1, 165.0, 0.0, 3.6000000000000001, "Chicken Breast", 31.0, 100.0 },
                    { 2, 208.0, 0.0, 13.0, "Salmon", 20.0, 100.0 },
                    { 3, 176.0, 0.0, 10.0, "Ground Beef (90% lean)", 20.0, 100.0 },
                    { 4, 78.0, 0.59999999999999998, 5.0, "Eggs", 6.0, 50.0 },
                    { 5, 116.0, 0.0, 1.0, "Tuna (canned)", 26.0, 100.0 },
                    { 6, 135.0, 0.0, 1.0, "Turkey Breast", 30.0, 100.0 },
                    { 7, 100.0, 6.0, 0.69999999999999996, "Greek Yogurt", 17.0, 150.0 },
                    { 8, 122.0, 12.0, 5.0, "Milk (2%)", 8.0, 240.0 },
                    { 9, 98.0, 3.3999999999999999, 4.2999999999999998, "Cottage Cheese", 11.0, 100.0 },
                    { 10, 113.0, 0.40000000000000002, 9.0, "Cheddar Cheese", 7.0, 28.0 },
                    { 11, 165.0, 35.0, 1.5, "Brown Rice (cooked)", 3.5, 150.0 },
                    { 12, 195.0, 45.0, 0.40000000000000002, "White Rice (cooked)", 4.0, 150.0 },
                    { 13, 158.0, 27.0, 3.0, "Oatmeal (cooked)", 6.0, 175.0 },
                    { 14, 81.0, 14.0, 1.0, "Whole Wheat Bread", 4.0, 30.0 },
                    { 15, 220.0, 43.0, 1.3, "Pasta (cooked)", 8.0, 150.0 },
                    { 16, 129.0, 30.0, 0.10000000000000001, "Sweet Potato", 2.0, 150.0 },
                    { 17, 180.0, 30.0, 2.5, "Quinoa (cooked)", 6.0, 150.0 },
                    { 18, 34.0, 7.0, 0.40000000000000002, "Broccoli", 2.7999999999999998, 100.0 },
                    { 19, 23.0, 3.6000000000000001, 0.40000000000000002, "Spinach", 2.8999999999999999, 100.0 },
                    { 20, 31.0, 7.0, 0.10000000000000001, "Green Beans", 1.8, 100.0 },
                    { 21, 41.0, 10.0, 0.20000000000000001, "Carrots", 0.90000000000000002, 100.0 },
                    { 22, 31.0, 6.0, 0.29999999999999999, "Bell Pepper", 1.0, 100.0 },
                    { 23, 105.0, 27.0, 0.40000000000000002, "Banana", 1.3, 120.0 },
                    { 24, 78.0, 21.0, 0.20000000000000001, "Apple", 0.40000000000000002, 150.0 },
                    { 25, 62.0, 15.0, 0.20000000000000001, "Orange", 1.2, 130.0 },
                    { 26, 57.0, 14.0, 0.29999999999999999, "Blueberries", 0.69999999999999996, 100.0 },
                    { 27, 32.0, 7.7000000000000002, 0.29999999999999999, "Strawberries", 0.69999999999999996, 100.0 },
                    { 28, 164.0, 6.0, 14.0, "Almonds", 6.0, 28.0 },
                    { 29, 188.0, 6.0, 16.0, "Peanut Butter", 8.0, 32.0 },
                    { 30, 185.0, 4.0, 18.0, "Walnuts", 4.2999999999999998, 28.0 }
                });

            migrationBuilder.InsertData(
                table: "ExerciseTemplates",
                columns: new[] { "Id", "ExerciseCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Barbell Bench Press" },
                    { 2, 1, "Incline Bench Press" },
                    { 3, 1, "Decline Bench Press" },
                    { 4, 1, "Dumbbell Bench Press" },
                    { 5, 1, "Dumbbell Flys" },
                    { 6, 1, "Cable Crossover" },
                    { 7, 1, "Push-Ups" },
                    { 8, 1, "Chest Dips" },
                    { 9, 1, "Machine Chest Press" },
                    { 10, 1, "Pec Deck Machine" },
                    { 11, 2, "Deadlift" },
                    { 12, 2, "Barbell Row" },
                    { 13, 2, "Dumbbell Row" },
                    { 14, 2, "Lat Pulldown" },
                    { 15, 2, "Pull-Ups" },
                    { 16, 2, "Chin-Ups" },
                    { 17, 2, "Seated Cable Row" },
                    { 18, 2, "T-Bar Row" },
                    { 19, 2, "Face Pulls" },
                    { 20, 2, "Hyperextensions" },
                    { 21, 3, "Barbell Squat" },
                    { 22, 3, "Front Squat" },
                    { 23, 3, "Leg Press" },
                    { 24, 3, "Romanian Deadlift" },
                    { 25, 3, "Leg Curl" },
                    { 26, 3, "Leg Extension" },
                    { 27, 3, "Walking Lunges" },
                    { 28, 3, "Bulgarian Split Squat" },
                    { 29, 3, "Calf Raises" },
                    { 30, 3, "Goblet Squat" },
                    { 31, 3, "Hip Thrust" },
                    { 32, 3, "Hack Squat" },
                    { 33, 4, "Overhead Press" },
                    { 34, 4, "Dumbbell Shoulder Press" },
                    { 35, 4, "Arnold Press" },
                    { 36, 4, "Lateral Raises" },
                    { 37, 4, "Front Raises" },
                    { 38, 4, "Reverse Flys" },
                    { 39, 4, "Upright Rows" },
                    { 40, 4, "Shrugs" },
                    { 41, 4, "Machine Shoulder Press" },
                    { 42, 5, "Barbell Curl" },
                    { 43, 5, "Dumbbell Curl" },
                    { 44, 5, "Hammer Curl" },
                    { 45, 5, "Preacher Curl" },
                    { 46, 5, "Concentration Curl" },
                    { 47, 5, "Cable Curl" },
                    { 48, 5, "Tricep Pushdown" },
                    { 49, 5, "Skull Crushers" },
                    { 50, 5, "Overhead Tricep Extension" },
                    { 51, 5, "Tricep Dips" },
                    { 52, 5, "Close-Grip Bench Press" },
                    { 53, 5, "Diamond Push-Ups" },
                    { 54, 6, "Crunches" },
                    { 55, 6, "Plank" },
                    { 56, 6, "Russian Twists" },
                    { 57, 6, "Leg Raises" },
                    { 58, 6, "Mountain Climbers" },
                    { 59, 6, "Bicycle Crunches" },
                    { 60, 6, "Dead Bug" },
                    { 61, 6, "Ab Rollout" },
                    { 62, 6, "Cable Woodchop" },
                    { 63, 6, "Hanging Knee Raises" },
                    { 64, 7, "Treadmill Running" },
                    { 65, 7, "Stationary Bike" },
                    { 66, 7, "Elliptical" },
                    { 67, 7, "Rowing Machine" },
                    { 68, 7, "Stair Climber" },
                    { 69, 7, "Jump Rope" },
                    { 70, 7, "Burpees" },
                    { 71, 7, "Box Jumps" },
                    { 72, 7, "Kettlebell Swings" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplates_ExerciseCategoryId",
                table: "ExerciseTemplates",
                column: "ExerciseCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseTemplates");

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExerciseCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
